using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net.Mail;
using InetSolution.Web;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class user_reactivate : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                m_txtName.Focus();
            }
        }

        protected void OnReactivate(object sender, EventArgs e)
        {
            StringBuilder oBody = new StringBuilder();
            oBody.AppendLine("This customer has requested that their deactivated account be reactivated.");
            oBody.AppendLine();
            oBody.AppendLine(string.Format("Name: {0}", m_txtName.Text));
            oBody.AppendLine(string.Format("Email: {0}", m_txtUsername.Text));
            oBody.AppendLine(string.Format("Phone: {0}", m_txtPhone.Text));
            oBody.AppendLine(string.Format("Company: {0}", m_txtCompanyName.Text));
            oBody.AppendLine(string.Format("Message: {0}", m_txtMessage.Text));

            Business.Services.EmailCommunicationService.UserReactivateNoticeSend(oBody);         

            ShowMessage("Thank you, we have received your re-activation request, we'll get back to you shortly.", MessageTone.Positive);
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