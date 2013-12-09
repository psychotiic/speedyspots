using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using Telerik.Web.UI;
using SpeedySpots.DataAccess;

namespace SpeedySpots
{
    public partial class site_settings : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();

                Business.Services.SitePropertiesService propertiesService = new Business.Services.SitePropertiesService();
                m_txtRequestThreshold.Text = MemberProtect.Utility.FormatInteger(propertiesService.RecutRequestThreshold);
                m_txtRequestActivityInterval.Text = MemberProtect.Utility.FormatInteger(propertiesService.ActivityInterval);
                m_txtFeedbackQuestion.Text = propertiesService.FeedbackEmailQuestion;
                m_txtFeedbackProblem.Text = propertiesService.FeedbackEmailProblem;
                m_txtEmailSystemNotifications.Text = propertiesService.EmailSystemNotifications;
                m_txtEmailBillings.Text = propertiesService.EmailBillings;
                m_txtEmailEstimatePayment.Text = propertiesService.EmailEstimatePayment;
            }
        }

        protected void OnAdd(object sender, EventArgs e)
        {
            Response.Redirect("~/email-templates-modify.aspx");
        }

        protected void OnSave(object sender, EventArgs e)
        {
            Business.Services.SitePropertiesService propertiesService = new Business.Services.SitePropertiesService();

            IAProperty oIAProperty = propertiesService.GetSiteProperties(true);
            oIAProperty.RecutRequestThreshold = MemberProtect.Utility.ValidateInteger(m_txtRequestThreshold.Text);
            oIAProperty.ActivityInterval = MemberProtect.Utility.ValidateInteger(m_txtRequestActivityInterval.Text);
            oIAProperty.FeedbackEmailQuestion = ApplicationContext.CleanAddressList(m_txtFeedbackQuestion.Text);
            oIAProperty.FeedbackEmailProblem = ApplicationContext.CleanAddressList(m_txtFeedbackProblem.Text);
            oIAProperty.EmailSystemNotifications = ApplicationContext.CleanAddressList(m_txtEmailSystemNotifications.Text);
            oIAProperty.EmailBillings = ApplicationContext.CleanAddressList(m_txtEmailBillings.Text);
            oIAProperty.EmailEstimatePayment = ApplicationContext.CleanAddressList(m_txtEmailEstimatePayment.Text);
            propertiesService.UpdateSiteProperties(oIAProperty);

            ShowMessage("Settings saved.", InetSolution.Web.MessageTone.Positive);
        }

        #region Grid
        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            m_grdList.DataSource = DataAccess.fn_Admin_GetEmailEstimates();
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;
            }
        }

        protected void OnItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (e.CommandName == "View")
                {
                    GridDataItem oDataItem = e.Item as GridDataItem;

                    Response.Redirect(string.Format("~/email-templates-modify.aspx?id={0}", oDataItem["IAEmailTemplateID"].Text));
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