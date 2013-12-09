using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetSolution.Web;
using SpeedySpots.InetActive.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots.InetActive
{
    public partial class SiteParams : InetActiveBasePage
    {
        private IAProperty                          m_oIAProperty = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            m_oIAProperty = DataAccess.IAProperties.SingleOrDefault();

            if(!IsPostBack)
            {
                if(m_oIAProperty != null)
                {
                    m_txtSiteName.Text = m_oIAProperty.SiteName;
                    m_txtHostName.Text = m_oIAProperty.HostName;
                    m_txtSiteDomain.Text = m_oIAProperty.SiteDomain;
                    m_txtEmailNewAccount.Text = m_oIAProperty.EmailNewAccount;
                    m_txtEmailAddressFrom.Text = m_oIAProperty.EmailAddressFrom;
                    m_txtEmailAddressFromName.Text = m_oIAProperty.EmailAddressFromName;
                    m_chkAuthorizeNetIsDebug.Checked = m_oIAProperty.AuthorizeNetIsDebug;
                    m_txtAuthorizeNetLoginID.Text = MemberProtect.Cryptography.Decrypt(m_oIAProperty.AuthorizeNetLoginID);
                    m_txtAuthorizeNetTransactionKey.Text = MemberProtect.Cryptography.Decrypt(m_oIAProperty.AuthorizeNetTransactionKey);
                }

                m_txtSmtpHost.Text = MemberProtect.Email.SmtpHost;
                m_txtSmtpPort.Text = MemberProtect.Utility.FormatInteger(MemberProtect.Email.SmtpPort);
                m_txtSmtpUsername.Text = MemberProtect.Email.SmtpUsername;
                m_txtSmtpPassword.Text = MemberProtect.Email.SmtpPassword;
                m_txtEmailNewAccount.Text = m_oIAProperty.EmailNewAccount;
                m_txtEmailAddressFrom.Text = m_oIAProperty.EmailAddressFrom;
                m_txtEmailAddressFromName.Text = m_oIAProperty.EmailAddressFromName;
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            if(m_oIAProperty == null)
            {
                m_oIAProperty = new IAProperty();
                DataAccess.IAProperties.InsertOnSubmit(m_oIAProperty);
            }

            MemberProtect.Email.SetSmtpSettings(m_txtSmtpHost.Text, MemberProtect.Utility.ValidateInteger(m_txtSmtpPort.Text), m_txtSmtpUsername.Text, m_txtSmtpPassword.Text);

            // Save Information
            m_oIAProperty.SiteName = m_txtSiteName.Text;
            m_oIAProperty.HostName = m_txtHostName.Text;
            m_oIAProperty.SiteDomain = m_txtSiteDomain.Text;
            m_oIAProperty.EmailNewAccount = m_txtEmailNewAccount.Text;
            m_oIAProperty.EmailAddressFrom = m_txtEmailAddressFrom.Text;
            m_oIAProperty.EmailAddressFromName = m_txtEmailAddressFromName.Text;
            m_oIAProperty.AuthorizeNetIsDebug = m_chkAuthorizeNetIsDebug.Checked;
            m_oIAProperty.AuthorizeNetLoginID = MemberProtect.Cryptography.Encrypt(m_txtAuthorizeNetLoginID.Text);
            m_oIAProperty.AuthorizeNetTransactionKey = MemberProtect.Cryptography.Encrypt(m_txtAuthorizeNetTransactionKey.Text);

            DataAccess.SubmitChanges();

            ShowMessage("Site properties have been saved.", MessageTone.Positive);
        }
    }
}
