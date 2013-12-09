using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;
using SpeedySpots.Business.Services;
using Telerik.Web.UI;

namespace SpeedySpots
{
    public partial class staff_dashboard_new : SiteBasePage
    {
        public int InitalPageNumber { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDireciton { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ApplicationContext.IsStaff)
            {
                Response.Redirect("~/Default.aspx");
            }
            
            InitalPageNumber = 1;

            if (!Page.IsPostBack)
            {
                m_listUsersName.Text = string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName"));

                StaffRequestDashboardQuery query = Business.Services.StaffDashboardRequestsService.GetStaffRequestDashboardSettings(MemberProtect.CurrentUser.UserID, DataAccess);
                query.PageSize = ApplicationContext.GetGridPageSize();

                m_hdnPageSize.Value = query.PageSize.ToString();
                m_hdnSortName.Value = query.OrderBy.ToString();
                m_hdnSortDir.Value = query.OrderByDESC ? "DESC" : "ASC";

                ConfigureFilters();
                ConfigureMultiSelectFilters(query);
                
                LoadDashboard(query);
            }
        }

        private void ConfigureFilters()
        {
            SetDefaultButton(m_txtRequestNumber, m_btnFilter);
            SetDefaultButton(m_dtCreatedDate, m_btnFilter);        

            // Load status filter options
            m_lstStatus.DataSource = Business.Services.RequestsService.GetStatusOption(DataAccess);
            m_lstStatus.DataValueField = "ID";
            m_lstStatus.DataTextField = "Name";
            m_lstStatus.DataBind();
           

            // Lanage options
            Dictionary<string, string> oLanguages = new Dictionary<string, string>();
            oLanguages.Add("Unspecified", "Unspecified");
            oLanguages.Add("English", "English");
            oLanguages.Add("Spanish", "Spanish");

            m_lstLanugage.DataSource = oLanguages;
            m_lstLanugage.DataValueField = "Value";
            m_lstLanugage.DataTextField = "Value";
            m_lstLanugage.DataBind();


            // Load status filter options
            m_lstLabels.DataSource = Business.Services.LabelsService.GetLabelsForFilterDropDown(DataAccess);
            m_lstLabels.DataValueField = "IALabelID";
            m_lstLabels.DataTextField = "Text";
            m_lstLabels.DataBind();
        }

        private void LoadDashboard(StaffRequestDashboardQuery query)
        {
            m_RequestDashboard.Query = query;
            m_RequestDashboard.BindData();
        }

        private StaffRequestDashboardQuery BuildQuery()
        {
            StaffRequestDashboardQuery query = new StaffRequestDashboardQuery();

            query.PageSize = int.Parse(m_hdnPageSize.Value);
            query.RequestNumber = m_txtRequestNumber.Text;

            if (m_dtCreatedDate.SelectedDate.HasValue)
            {
                query.RequestDate = m_dtCreatedDate.SelectedDate.Value;
            }
            
            query.RequestedBy = m_txtRequestedBy.Text;

            query.Lanugages = GetSelectedLanguageOptions();
            query.SetStatus(GetSelectedStatusOptions());
            query.SetLabels(GetSelectedLabelOptions());
            query.SetOrderBy(m_hdnSortName.Value + " " + m_hdnSortDir.Value);
                        
            return query;
        }

        private void ConfigureMultiSelectFilters(StaffRequestDashboardQuery query)
        {
            m_hdnLanugage.Value = query.LanugagesAsStringList;
            m_hdnStatus.Value = query.StatusAsStringList;
            m_hdnLabels.Value = query.LabelsAsStringList;            
        }

        protected void OnFilterRequests(object sender, EventArgs e)
        {
            StaffRequestDashboardQuery query = BuildQuery();
           
            LoadDashboard(query);

            //Re-save this user's preferences
            Business.Services.StaffDashboardRequestsService.UpdateStaffRequestDashboardSettings(query, MemberProtect.CurrentUser.UserID, DataAccess);

            // Update our labels as the business rules for query may have made some changes
            m_hdnLanugage.Value = query.LanugagesAsStringList;
            m_hdnStatus.Value = query.StatusAsStringList;
            m_hdnLabels.Value = query.LabelsAsStringList;
        }

        public List<int> GetSelectedStatusOptions()
        {
            List<int> oValues = new List<int>();
            if (!string.IsNullOrEmpty(m_hdnStatus.Value))
            {
                m_hdnStatus.Value = m_hdnStatus.Value.TrimStart(',');
                oValues.AddRange(m_hdnStatus.Value.Split(',').Select(i => int.Parse(i)));
            }

            return oValues;
        }

        public List<int> GetSelectedLabelOptions()
        {
            List<int> oValues = new List<int>();

            if (!string.IsNullOrEmpty(m_hdnLabels.Value))
            {
                m_hdnLabels.Value = m_hdnLabels.Value.TrimStart(',');
                if (LabelsShowAll())
                {
                    oValues.Add(-1);
                }
                else if (LabelsShowUnlabeledOnly())
                {
                    oValues.Add(-2);
                }
                else
                {
                    oValues.AddRange(m_hdnLabels.Value.Split(',').Select(i => int.Parse(i)));
                }
            }
            else
            {
                oValues.Add(-1);
            }

            return oValues;
        }

        private bool LabelsShowAll()
        {
            // Check if "All" is selected or nothing is selected
            return (m_hdnLabels.Value.Length <= 0 || m_hdnLabels.Value.IndexOf("-1") >= 0);
        }

        public bool LabelsShowUnlabeledOnly()
        {
            bool returnValue = false;
            if (m_hdnLabels.Value.Length > 0)
            {
                // Check if "Unlabeled" is selected
                returnValue = (m_hdnLabels.Value.IndexOf("-2") >= 0);
            }
            
            return returnValue;
        }

        public List<string> GetSelectedLanguageOptions()
        {
            List<string> oValues = new List<string>();
            if (!string.IsNullOrEmpty(m_hdnLanugage.Value))
            {
                m_hdnLanugage.Value = m_hdnLanugage.Value.TrimStart(',');
                oValues.AddRange(m_hdnLanugage.Value.Split(','));
            }

            return oValues;
        }
        
    }
}