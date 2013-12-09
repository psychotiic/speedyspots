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
    public partial class user_upgrade : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SetDefaultButton(m_txtUsername, m_btnLogin);
                SetDefaultButton(m_txtPassword, m_btnLogin);
                m_txtUsername.Focus();

                if(!string.IsNullOrEmpty(Request.QueryString["u"]))
                {
                    tblSecUser oSecUser = DataAccess.tblSecUsers.SingleOrDefault(row => row.IDUser == Request.QueryString["u"]);
                    if(oSecUser != null)
                    {
                        m_txtUsername.Text = oSecUser.IDUser;
                        m_txtPassword.Text = oSecUser.Password;

                        OnLogin(this, new EventArgs());
                    }
                    else
                    {
                        RedirectMessage("~/user-upgrade.aspx", "We're sorry, but that user name was not found.", MessageTone.Negative);
                    }
                }
            }
        }

        protected void OnLogin(object sender, EventArgs e)
        {
            tblSecUser oSecUser = DataAccess.tblSecUsers.SingleOrDefault(row => row.IDUser == m_txtUsername.Text && row.Password == m_txtPassword.Text);
            if(oSecUser != null)
            {
                UpgradeAccount oUpgradeAccount = new UpgradeAccount();
                oUpgradeAccount.ID = oSecUser.ID;
                oUpgradeAccount.UserID = oSecUser.IDUser;
                oUpgradeAccount.Email = oSecUser.Email;
                oUpgradeAccount.Password = oSecUser.Password;
                oUpgradeAccount.EmailContacts = oSecUser.EmailNotifyList;

                // By default, all users are pay first, to be safe, if they are otherwise the following code will override
                oUpgradeAccount.IsPayFirst = true;

                if(oSecUser.tblCustomerDepartment != null)
                {
                    if(oSecUser.tblCustomerDepartment.tblCustomer != null)
                    {
                        if(oSecUser.tblCustomerDepartment.tblCustomer.PayFirst.HasValue)
                        {
                            oUpgradeAccount.IsPayFirst = oSecUser.tblCustomerDepartment.tblCustomer.PayFirst.Value;
                        }
                    }
                }

                Session["UpgradeAccount"] = oUpgradeAccount;
                RedirectMessage("~/user-registration.aspx", "Please continue upgrading your new account.", MessageTone.Positive);
            }
            else
            {
                ShowMessage("User account could not be found.", MessageTone.Negative);
            }
        }

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