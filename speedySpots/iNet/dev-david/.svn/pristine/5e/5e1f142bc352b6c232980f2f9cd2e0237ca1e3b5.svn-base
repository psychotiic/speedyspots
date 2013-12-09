using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class email_templates_modify : SiteBasePage
    {
        private IAEmailTemplate                     m_oIAEmailTemplateID = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                if(ApplicationContext.IsAdmin && ApplicationContext.IsStaff)
                {
                    m_oIAEmailTemplateID = DataAccess.IAEmailTemplates.SingleOrDefault(row => row.IAEmailTemplateID == MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]));
                }
            }

            if(!IsPostBack)
            {
                if(m_oIAEmailTemplateID != null)
                {
                    m_txtName.Text = m_oIAEmailTemplateID.Name;
                    m_txtBody.Content = m_oIAEmailTemplateID.Body;
                }
                else
                {
                    m_txtName.Focus();
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            if(m_oIAEmailTemplateID == null)
            {
                m_oIAEmailTemplateID = new IAEmailTemplate();
                m_oIAEmailTemplateID.CreatedDateTime = DateTime.Now;
                DataAccess.IAEmailTemplates.InsertOnSubmit(m_oIAEmailTemplateID);
            }

            m_oIAEmailTemplateID.Name = m_txtName.Text;
            m_oIAEmailTemplateID.Body = m_txtBody.Content;
            DataAccess.SubmitChanges();

            RedirectMessage("~/site-settings.aspx", "Email template saved.", InetSolution.Web.MessageTone.Positive);
        }

        protected void OnBack(object sender, EventArgs e)
        {
            Response.Redirect("~/site-settings.aspx");
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            DataAccess.IAEmailTemplates.DeleteOnSubmit(m_oIAEmailTemplateID);
            DataAccess.SubmitChanges();

            RedirectMessage("~/site-settings.aspx", "Email template deleted.", InetSolution.Web.MessageTone.Positive);
        }

        #region Public Properties
        public bool IsNew
        {
            get
            {
                if(m_oIAEmailTemplateID == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region Overridden Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Admin);

            return oAccessControl;
        }
        #endregion
    }
}