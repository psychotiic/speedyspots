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
    public partial class label_modify : SiteBasePage
    {
        private IALabel                             m_oIALabel = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                if(ApplicationContext.IsAdmin || ApplicationContext.IsStaff)
                {
                    m_oIALabel = DataAccess.IALabels.SingleOrDefault(row => row.IALabelID == MemberProtect.Utility.ValidateInteger(Request.QueryString["id"]));
                }
            }

            if(!IsPostBack)
            {
                SetDefaultButton(m_txtText, m_btnSubmit);

                if(m_oIALabel != null)
                {
                    m_txtText.Text = m_oIALabel.Text;
                    m_chkIsCustomerVisible.Checked = m_oIALabel.IsCustomerVisible;
                }
                else
                {
                    m_txtText.Focus();
                }
            }
        }

        protected void OnSave(object sender, EventArgs e)
        {
            if(m_txtText.Text.ToUpper() == "ALL" || m_txtText.Text.ToUpper() == "UNLABELED")
            {
                ShowMessage("Label may not contain 'All' or 'Unlabeled' as those are use within the system.", InetSolution.Web.MessageTone.Negative);
                return;
            }

            int iIALabelID = 0;
            if(m_oIALabel != null)
            {
                iIALabelID = m_oIALabel.IALabelID;
            }

            if(DataAccess.IALabels.Count(row => row.Text == m_txtText.Text && row.IALabelID != iIALabelID) > 0)
            {
                ShowMessage(string.Format("The label '{0}' already exists.", m_txtText.Text), InetSolution.Web.MessageTone.Negative);
                return;
            }

            if(IsNew)
            {
                m_oIALabel = new IALabel();
                DataAccess.IALabels.InsertOnSubmit(m_oIALabel);
            }

            m_oIALabel.Text = m_txtText.Text;
            m_oIALabel.IsCustomerVisible = m_chkIsCustomerVisible.Checked;
            DataAccess.SubmitChanges();

            Business.Services.LabelsService.FlushLabelsCache();

            RedirectMessage("~/labels.aspx", "Label information saved.", InetSolution.Web.MessageTone.Positive);
        }

        protected void OnBack(object sender, EventArgs e)
        {
            Response.Redirect("~/labels.aspx");
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            DataAccess.IALabels.DeleteOnSubmit(m_oIALabel);
            DataAccess.SubmitChanges();

            RedirectMessage("~/labels.aspx", "Label deleted.", InetSolution.Web.MessageTone.Positive);
        }

        #region Public Properties
        public bool IsNew
        {
            get
            {
                if(m_oIALabel == null)
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
            oAccessControl.Add(AccessControl.Staff);

            return oAccessControl;
        }
        #endregion
    }
}