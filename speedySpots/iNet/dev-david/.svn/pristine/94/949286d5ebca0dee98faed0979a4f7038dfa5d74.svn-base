using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using SpeedySpots.DataAccess;
using SpeedySpots.Objects;

namespace SpeedySpots
{
    public partial class login_reset : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_txtUsername.Focus();
            }
        }

        protected void OnReset(object sender, EventArgs e)
        {
            if(MemberProtect.User.Exists(m_txtUsername.Text))
            {
                Random oRandom = new Random();
                string sPassword = oRandom.Next(100000, 999999).ToString();

                MemberProtect.User.ChangePassword(MemberProtect.User.GetUserID(m_txtUsername.Text), sPassword);
                string sTemporaryPassword = MemberProtect.User.GenerateTemporaryPassword(m_txtUsername.Text);
                
                StringBuilder oSB = new StringBuilder();
                oSB.AppendLine(string.Format("Hello {0},", MemberProtect.User.GetDataItem(m_txtUsername.Text, "FirstName")));
                oSB.AppendLine();
                oSB.AppendLine("Your account password has been reset, please use the following temporary password to login, once you do so you will be asked to change your password.");
                oSB.AppendLine(string.Format("Temporary Password: {0}", sTemporaryPassword));
                oSB.AppendLine();
                oSB.AppendLine("Thank you,");
                oSB.AppendLine("SpeedySpots");

                Business.Services.EmailCommunicationService.PasswordResetNoticeSend(oSB, m_txtUsername.Text);

                RedirectMessage("~/login.aspx", "Your password has been reset, an email has been sent with a temporary password you may login with.", InetSolution.Web.MessageTone.Positive);
            }
            else
            {
                ShowMessage("Email address does not exist.");
                m_txtUsername.Text = string.Empty;
                m_txtUsername.Focus();
            }
        }

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Public);

            return oAccessControl;
        }
        #endregion
    }
}