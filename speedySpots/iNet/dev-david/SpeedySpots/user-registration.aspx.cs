using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class user_registration : SiteBasePage
    {
        private UpgradeAccount                      m_oUpgradeAccount = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["UpgradeAccount"] != null)
            {
                m_oUpgradeAccount = (UpgradeAccount)Session["UpgradeAccount"];
            }

            ApplicationContext.LoadCountries(ref m_cboCountry);
            ApplicationContext.LoadCountries(ref m_cboBillingCountry);

            if(!IsPostBack)
            {
				m_txtUsername.Focus();

                if(m_oUpgradeAccount != null)
                {
                    divUserMessage.Visible = false;
                    m_hdnPastID.Value = m_oUpgradeAccount.UserID;
                    //m_txtUsername.Text = m_oUpgradeAccount.Email;
                    //m_txtPassword.Attributes["value"] = m_oUpgradeAccount.Password;
                    //m_txtVerifyPassword.Attributes["value"] = m_oUpgradeAccount.Password;

                    // Check if this user's organization has been imported from Quickbooks, if it has, auto-associate with the already existing organization
                    if(HasOrgAssociation())
                    {
                        Guid oMPOrgID = GetOrgAssociation();

                        // Load up existing organization information
                        m_txtCompanyName.Text = MemberProtect.Organization.GetName(oMPOrgID);
                        m_txtAddressLine1.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "Address1");
                        m_txtAddressLine2.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "Address2");
                        m_txtCity.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "City");
                        m_cboState.SelectedValue = MemberProtect.Organization.GetDataItem(oMPOrgID, "State");
                        m_cboCountry.SelectedValue = MemberProtect.Organization.GetDataItem(oMPOrgID, "Country");
                        m_txtZip.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "Zip");
                        m_txtCompanyPhone.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "Phone");
                        m_txtFax.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "Fax");

                        m_txtBillingAddress1.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingAddress1");
                        m_txtBillingAddress2.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingAddress2");
                        m_txtBillingCity.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingCity");
                        m_cboBillingState.SelectedValue = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingState");
                        m_cboBillingCountry.SelectedValue = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingCountry");
                        m_txtBillingZip.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingZip");
                        m_txtBillingPhone.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingPhone");
                        m_txtEmailInvoice.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "EmailInvoice");
                        m_txtBillingName.Text = MemberProtect.Organization.GetDataItem(oMPOrgID, "BillingName");

                        DisableCompanyControls();
                    }
                }
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            // Validate username
            if(!MemberProtect.User.Exists(m_txtUsername.Text))
            {
                Guid oUserID = MemberProtect.User.Add(m_txtUsername.Text, m_txtPassword.Text);
                MemberProtect.User.SetDataItem(oUserID, "FirstName", m_txtFirstName.Text);
                MemberProtect.User.SetDataItem(oUserID, "LastName", m_txtLastName.Text);
                MemberProtect.User.SetDataItem(oUserID, "Phone", m_txtPhone.Text);
                MemberProtect.User.SetDataItem(oUserID, "PhoneExtension", m_txtPhoneExt.Text);
                MemberProtect.User.SetDataItem(oUserID, "MobilePhone", m_txtMobilePhone.Text);
                MemberProtect.User.SetDataItem(oUserID, "Comments", string.Empty);
                MemberProtect.User.SetDataItem(oUserID, "Department", m_txtDepartment.Text);
                MemberProtect.User.SetDataItem(oUserID, "IsCustomer", MemberProtect.Utility.BoolToYesNo(true));
                MemberProtect.User.SetDataItem(oUserID, "IsStaff", MemberProtect.Utility.BoolToYesNo(false));
                MemberProtect.User.SetDataItem(oUserID, "IsTalent", MemberProtect.Utility.BoolToYesNo(false));
                MemberProtect.User.SetDataItem(oUserID, "IsAdmin", MemberProtect.Utility.BoolToYesNo(false));
                MemberProtect.User.SetDataItem(oUserID, "IsArchived", MemberProtect.Utility.BoolToYesNo(false));

                if(HasOrgAssociation())
                {
                    // Assign the new user to the existing organization
                    MemberProtect.User.AssignOrganization(oUserID, GetOrgAssociation());
                }
                else
                {
                    // Create new organization
                    Guid oOrgTypeID = MemberProtect.Organization.GetTypeID("Client");
                    Guid oOrgID = MemberProtect.Organization.Add(m_txtCompanyName.Text, oOrgTypeID);
                    MemberProtect.Organization.SetDataItem(oOrgID, "AccessKey", string.Format("{0}", new Random().Next(100000, 999999)));
                    MemberProtect.Organization.SetDataItem(oOrgID, "Address1", m_txtAddressLine1.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "Address2", m_txtAddressLine2.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "City", m_txtCity.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "State", m_cboState.SelectedValue);
                    MemberProtect.Organization.SetDataItem(oOrgID, "Country", m_cboCountry.SelectedValue);
                    MemberProtect.Organization.SetDataItem(oOrgID, "Zip", m_txtZip.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "Phone", m_txtCompanyPhone.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "Fax", m_txtFax.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingAddress1", m_txtBillingAddress1.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingAddress2", m_txtBillingAddress2.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingCity", m_txtBillingCity.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingState", m_cboBillingState.SelectedValue);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingCountry", m_cboBillingCountry.SelectedValue);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingZip", m_txtBillingZip.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingName", m_txtBillingName.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "BillingPhone", m_txtBillingPhone.Text);
                    MemberProtect.Organization.SetDataItem(oOrgID, "IsVerified", MemberProtect.Utility.BoolToYesNo(false));
                    MemberProtect.Organization.SetDataItem(oOrgID, "EmailInvoice", ApplicationContext.CleanAddressList(m_txtEmailInvoice.Text));
                    MemberProtect.Organization.SetDataItem(oOrgID, "IsArchived", MemberProtect.Utility.BoolToYesNo(false));

                    if(m_oUpgradeAccount != null)
                    {
                        MemberProtect.Organization.SetDataItem(oOrgID, "IsPayFirst", MemberProtect.Utility.BoolToYesNo(m_oUpgradeAccount.IsPayFirst));
                    }
                    else
                    {
                        MemberProtect.Organization.SetDataItem(oOrgID, "IsPayFirst", MemberProtect.Utility.BoolToYesNo(true));
                    }

                    // Assign the new user to the new organization
                    MemberProtect.User.AssignOrganization(oUserID, oOrgID);

                    // Save users billing information
                    //IACustomerCreditCard oIACustomerCreditCard = new IACustomerCreditCard();
                    //oIACustomerCreditCard.MPUserID = oUserID;
                    //oIACustomerCreditCard.Name = "Default";
                    //oIACustomerCreditCard.CreditCardType = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //oIACustomerCreditCard.CreditCardNumber = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //oIACustomerCreditCard.CreditCardExpirationMonth = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //oIACustomerCreditCard.CreditCardExpirationYear = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //oIACustomerCreditCard.CreditCardFirstName = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //oIACustomerCreditCard.CreditCardLastName = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //oIACustomerCreditCard.CreditCardZip = MemberProtect.Cryptography.Encrypt(string.Empty);
                    //DataAccess.IACustomerCreditCards.InsertOnSubmit(oIACustomerCreditCard);
                    DataAccess.SubmitChanges();
                }

                if(m_oUpgradeAccount != null)
                {
                    tblSecUser oSecUser = DataAccess.tblSecUsers.SingleOrDefault(row => row.ID == m_oUpgradeAccount.ID);
                    if (oSecUser != null && !oSecUser.IsUpgraded)
                    {
                        oSecUser.IsUpgraded = true;
                        oSecUser.MPUserID = oUserID;
                        DataAccess.SubmitChanges();
                    }

                    Session.Remove("UpgradeAccount");
                }

                RedirectMessage("~/login.aspx", "Your account has been successfully created, you may now login.", InetSolution.Web.MessageTone.Positive);
            }
            else
            {
                if(MemberProtect.Utility.YesNoToBool(MemberProtect.User.GetDataItem(m_txtUsername.Text, "IsArchived")))
                {
                    SetMessage(string.Format("Email address already exists and is deactivated. <a href='{0}'>Request it be re-activated here</a>", ResolveUrl("~/user-reactivate.aspx")), InetSolution.Web.MessageTone.Negative);
                }
                else
                {
                    SetMessage("Email address already exists.", InetSolution.Web.MessageTone.Negative);
                }
            }
        }

        #region Private Methods
        private Guid GetOrgAssociation()
        {
            if(m_oUpgradeAccount != null)
            {
                UserOrgLink oUserOrgLink = DataAccess.UserOrgLinks.SingleOrDefault(row => row.PastUserID == m_oUpgradeAccount.UserID);
                if(oUserOrgLink != null)
                {
                    if(MemberProtect.Organization.Exists(oUserOrgLink.MPOrgID))
                    {
                        return oUserOrgLink.MPOrgID;
                    }
                }
            }

            return Guid.Empty;
        }

        private bool HasOrgAssociation()
        {
            Guid oMPOrgID = GetOrgAssociation();
            if(oMPOrgID != Guid.Empty)
            {
                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(m_hdnPastID.Value))
                {
                    return PopulateUpgradeInfo(m_hdnPastID.Value);
                }
                else
                {
                    return false;
                }
            }
        }

        private bool PopulateUpgradeInfo(string m_sUserID)
        {
            tblSecUser oSecUser = DataAccess.tblSecUsers.SingleOrDefault(row => row.IDUser == m_sUserID);
            if (oSecUser != null)
            {
                UpgradeAccount oUpgradeAccount = new UpgradeAccount();
                oUpgradeAccount.ID = oSecUser.ID;
                oUpgradeAccount.UserID = oSecUser.IDUser;
                oUpgradeAccount.Email = oSecUser.Email;
                oUpgradeAccount.Password = oSecUser.Password;
                oUpgradeAccount.EmailContacts = oSecUser.EmailNotifyList;

                // By default, all users are pay first, to be safe, if they are otherwise the following code will override
                oUpgradeAccount.IsPayFirst = true;

                if (oSecUser.tblCustomerDepartment != null)
                {
                    if (oSecUser.tblCustomerDepartment.tblCustomer != null)
                    {
                        if (oSecUser.tblCustomerDepartment.tblCustomer.PayFirst.HasValue)
                        {
                            oUpgradeAccount.IsPayFirst = oSecUser.tblCustomerDepartment.tblCustomer.PayFirst.Value;
                        }
                    }
                }
                m_oUpgradeAccount = oUpgradeAccount;
                Session["UpgradeAccount"] = m_oUpgradeAccount;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void DisableCompanyControls()
        {
            // Disable the physical controls
            m_txtCompanyName.Enabled = false;
            m_txtAddressLine1.Enabled = false;
            m_txtAddressLine2.Enabled = false;
            m_txtCity.Enabled = false;
            m_cboState.Enabled = false;
            m_cboCountry.Enabled = false;
            m_txtZip.Enabled = false;
            m_txtCompanyPhone.Enabled = false;
            m_txtFax.Enabled = false;

            m_txtBillingAddress1.Enabled = false;
            m_txtBillingAddress2.Enabled = false;
            m_txtBillingCity.Enabled = false;
            m_cboBillingState.Enabled = false;
            m_cboBillingCountry.Enabled = false;
            m_txtBillingZip.Enabled = false;
            m_txtBillingPhone.Enabled = false;
            m_txtEmailInvoice.Enabled = false;
            m_txtBillingName.Enabled = false;

            // Disable the validation controls
            m_reqCompanyName.Enabled = false;
            m_expCompanyName.Enabled = false;
            m_reqAddressLine1.Enabled = false;
            m_expAddressLine1.Enabled = false;
            m_reqCity.Enabled = false;
            m_expCity.Enabled = false;
            m_reqState.Enabled = false;
            m_reqCountry.Enabled = false;
            m_reqZip.Enabled = false;
            m_reqCompanyPhone.Enabled = false;
            m_expBillingAddress1.Enabled = false;
            m_reqBillingAddress1.Enabled = false;
            m_expBillingCity.Enabled = false;
            m_reqBillingCity.Enabled = false;
            m_reqBillingState.Enabled = false;
            m_reqBillingCountry.Enabled = false;
            m_reqBillingZip.Enabled = false;
            m_expBillingName.Enabled = false;
            m_reqBillingName.Enabled = false;
            m_reqBillingPhone.Enabled = false;
            m_reqEmailInvoice.Enabled = false;
            m_expEmailInvoice.Enabled = false;

            m_divIsBillingSame.Visible = false;
        }
        #endregion

        #region Overridden Methods
        public override bool GetSSL()
        {
            return true;
        }

        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Public);

            return oAccessControl;
        }
        #endregion
    }
}
