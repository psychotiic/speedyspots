using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using SpeedySpots.Objects;
using Telerik.Web.UI;
using SpeedySpots.DataAccess;
using LinqKit;

namespace SpeedySpots
{
   using Business;
   using JobStatus = Business.JobStatus;

   public partial class staff_dashboard_completed : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // User must be a producer in order to visit this page
            if(!ApplicationContext.IsStaff)
            {
                Response.Redirect("~/Default.aspx");
            }

            if(!IsPostBack)
            {
                Dictionary<string, string> oJobStatuses = new Dictionary<string, string>();
                oJobStatuses.Add("Processing", "Processing");
                oJobStatuses.Add("In Production", "In Production");
                oJobStatuses.Add("Completed", "Completed");

                // Load status filter options
                m_cboRequestStatus.DataSource = oJobStatuses;
                m_cboRequestStatus.DataValueField = "Value";
                m_cboRequestStatus.DataTextField = "Value";
                m_cboRequestStatus.DataBind();

                Dictionary<string, string> oLanguages = new Dictionary<string, string>();
                oLanguages.Add("Unspecified", "Unspecified");
                oLanguages.Add("English", "English");
                oLanguages.Add("Spanish", "Spanish");

                m_cboRequestLanguage.DataSource = oLanguages;
                m_cboRequestLanguage.DataValueField = "Value";
                m_cboRequestLanguage.DataTextField = "Value";
                m_cboRequestLanguage.DataBind();

                oLanguages = new Dictionary<string, string>();
                oLanguages.Add("English", "English");
                oLanguages.Add("Spanish", "Spanish");

                // Load status filter options
                string sFilterString = string.Empty;
                ApplicationContext.LoadGridSettings("Staff Completed", m_grdRequests, out sFilterString);

                if(sFilterString != string.Empty)
                {
                    string[] sFilters = sFilterString.Split(ApplicationContext.Separator.ToCharArray());
                    foreach(string sFilterChunk in sFilters)
                    {
                        string[] sFilterParts = sFilterChunk.Split(ApplicationContext.SubSeparator.ToCharArray());
                        string sName = sFilterParts[0];
                        string sValue = sFilterParts[1];

                        if(sName == "Request Start Date")
                        {
                            DateTime oDateTime = new DateTime();
                            DateTime.TryParse(sValue, out oDateTime);

                            if(oDateTime.Year == 1)
                            {
                                m_dtCreatedStartDate.SelectedDate = null;
                            }
                            else
                            {
                                m_dtCreatedStartDate.SelectedDate = oDateTime;
                            }
                        }
                        else if(sName == "Request End Date")
                        {
                            DateTime oDateTime = new DateTime();
                            DateTime.TryParse(sValue, out oDateTime);

                            if(oDateTime.Year == 1)
                            {
                                m_dtCreatedEndDate.SelectedDate = null;
                            }
                            else
                            {
                                m_dtCreatedEndDate.SelectedDate = oDateTime;
                            }
                        }
                        else if(sName == "Completed Start Date")
                        {
                            DateTime oDateTime = new DateTime();
                            DateTime.TryParse(sValue, out oDateTime);

                            if(oDateTime.Year == 1)
                            {
                                m_dtCompletedStartDate.SelectedDate = null;
                            }
                            else
                            {
                                m_dtCompletedStartDate.SelectedDate = oDateTime;
                            }
                        }
                        else if(sName == "Completed End Date")
                        {
                            DateTime oDateTime = new DateTime();
                            DateTime.TryParse(sValue, out oDateTime);

                            if(oDateTime.Year == 1)
                            {
                                m_dtCompletedEndDate.SelectedDate = null;
                            }
                            else
                            {
                                m_dtCompletedEndDate.SelectedDate = oDateTime;
                            }
                        }
                        else if(sName == "Job Name")
                        {
                            m_txtJobName.Text = sValue;
                        }
                        else if(sName == "Status")
                        {
                            m_cboRequestStatus.SelectedValue = sValue;

                            string sText = string.Empty;
                            string sValues = string.Empty;
                            string[] oValues = m_cboRequestStatus.SelectedValue.Split(",".ToCharArray());
                            foreach(RadComboBoxItem oItem in m_cboRequestStatus.Items)
                            {
                                if(oValues.Contains(oItem.Value))
                                {
                                    CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;
                                    chkStatus.Checked = true;

                                    sText += oItem.Text + ",";
                                    sValues += oItem.Value + ",";
                                }
                            }

                            if(sText.Length > 0)
                            {
                                sText = sText.TrimEnd(",".ToCharArray());
                                sValues = sValues.TrimEnd(",".ToCharArray());

                                m_cboRequestStatus.Text = sText;
                                m_cboRequestStatus.SelectedValue = sValues;
                            }
                        }
                        else if(sName == "Languages")
                        {
                            m_cboRequestLanguage.SelectedValue = sValue;

                            string[] oValues = m_cboRequestLanguage.SelectedValue.Split(",".ToCharArray());
                            foreach(RadComboBoxItem oItem in m_cboRequestLanguage.Items)
                            {
                                if(oValues.Contains(oItem.Value))
                                {
                                    CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;
                                    chkStatus.Checked = true;
                                }
                            }

                            ProcessLanguages();
                        }
                        else if(sName == "ASAP")
                        {
                            m_chkRequestIsAsap.Checked = MemberProtect.Utility.YesNoToBool(sValue);
                        }
                    }
                }
                else
                {
                    // Defaults               
                    foreach(RadComboBoxItem oItem in m_cboRequestStatus.Items)
                    {
                        if(oItem.Value == "Completed")
                        {
                            CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;
                            chkStatus.Checked = true;
                        }
                    }

                    foreach(RadComboBoxItem oItem in m_cboRequestLanguage.Items)
                    {
                        CheckBox chkLanguage = oItem.FindControl("m_chkStatus") as CheckBox;
                        chkLanguage.Checked = true;
                    }

                    ProcessLanguages();

                    m_cboRequestStatus.Text = "Completed";
                    m_cboRequestStatus.SelectedValue = "Completed";
                }

                OnFilterRequests(this, new EventArgs());
            }
        }

        #region Grid (Requests)
        protected void OnFilterRequests(object sender, EventArgs e)
        {
            if(sender.GetType() != this.GetType())
            {
                StringBuilder oFilterString = new StringBuilder();
                if(m_cboRequestStatus.SelectedValue == string.Empty)
                {
                    // Due to custom text, we need to build the value string manually
                    string[] oText = m_cboRequestStatus.Text.Split(",".ToCharArray());
                    foreach(string sText in oText)
                    {
                        RadComboBoxItem oItem = m_cboRequestStatus.FindItemByText(sText);
                        if(oItem != null)
                        {
                            m_cboRequestStatus.SelectedValue += oItem.Value + ",";
                        }
                    }
                }

                m_cboRequestStatus.SelectedValue.TrimEnd(",".ToCharArray());
                m_cboRequestLanguage.SelectedValue.TrimEnd(",".ToCharArray());

                ProcessLanguages();

                oFilterString.Append(string.Format("Request Start Date{0}{1}{2}", ApplicationContext.SubSeparator, (m_dtCreatedStartDate.SelectedDate == null ? string.Empty : m_dtCreatedStartDate.SelectedDate.Value.ToString("g")), ApplicationContext.Separator));
                oFilterString.Append(string.Format("Request End Date{0}{1}{2}", ApplicationContext.SubSeparator, (m_dtCreatedEndDate.SelectedDate == null ? string.Empty : m_dtCreatedEndDate.SelectedDate.Value.ToString("g")), ApplicationContext.Separator));
                oFilterString.Append(string.Format("Completed Start Date{0}{1}{2}", ApplicationContext.SubSeparator, (m_dtCompletedStartDate.SelectedDate == null ? string.Empty : m_dtCompletedStartDate.SelectedDate.Value.ToString("g")), ApplicationContext.Separator));
                oFilterString.Append(string.Format("Completed End Date{0}{1}{2}", ApplicationContext.SubSeparator, (m_dtCompletedEndDate.SelectedDate == null ? string.Empty : m_dtCompletedEndDate.SelectedDate.Value.ToString("g")), ApplicationContext.Separator));
                oFilterString.Append(string.Format("Job Name{0}{1}{2}", ApplicationContext.SubSeparator, m_txtJobName.Text, ApplicationContext.Separator));
                oFilterString.Append(string.Format("Status{0}{1}{2}", ApplicationContext.SubSeparator, m_cboRequestStatus.SelectedValue, ApplicationContext.Separator));
                oFilterString.Append(string.Format("Languages{0}{1}{2}", ApplicationContext.SubSeparator, m_cboRequestLanguage.SelectedValue, ApplicationContext.Separator));
                oFilterString.Append(string.Format("ASAP{0}{1}{2}", ApplicationContext.SubSeparator, MemberProtect.Utility.BoolToYesNo(m_chkRequestIsAsap.Checked), ApplicationContext.Separator));

                // Remove last separator
                oFilterString.Remove(oFilterString.Length - 1, 1);

                ApplicationContext.SaveGridSettings("Staff Completed", oFilterString.ToString());
            }

            m_grdRequests.Rebind();
        }

        protected void OnNeedDataSourceRequests(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_Producer_GetCompletedDashboardResult> oResults = DataAccess.fn_Producer_GetCompletedDashboard();

            // Apply filters
            if(m_cboRequestLanguage.SelectedValue != string.Empty)
            {
                var oPredicate = PredicateBuilder.False<fn_Producer_GetCompletedDashboardResult>();

                string[] sLanguages = m_cboRequestLanguage.SelectedValue.Split(",".ToCharArray());
                foreach(string sLanguage in sLanguages)
                {
                    if(sLanguage == "Unspecified")
                    {
                        oPredicate = oPredicate.Or(row => true);
                    }
                    else if(sLanguage == "English")
                    {
                        oPredicate = oPredicate.Or(row => row.Language == "English");
                    }
                    else if(sLanguage == "Spanish")
                    {
                        oPredicate = oPredicate.Or(row => row.Language == "Spanish");
                    }
                }

                oResults = oResults.Where(oPredicate);
            }


            if(m_dtCreatedStartDate.SelectedDate.HasValue && m_dtCreatedEndDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date >= m_dtCreatedStartDate.SelectedDate.Value && row.CreatedDateTime.Date <= m_dtCreatedEndDate.SelectedDate.Value);
            }
            else if (m_dtCreatedStartDate.SelectedDate.HasValue && !m_dtCreatedEndDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date >= m_dtCreatedStartDate.SelectedDate.Value);
            }
            else if (!m_dtCreatedStartDate.SelectedDate.HasValue && m_dtCreatedEndDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date <= m_dtCreatedEndDate.SelectedDate.Value);
            }

            
            if(m_dtCompletedStartDate.SelectedDate.HasValue && m_dtCompletedEndDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CompletedDateTime.Date >= m_dtCompletedStartDate.SelectedDate.Value && row.CompletedDateTime.Date <= m_dtCompletedEndDate.SelectedDate.Value);
            }
            else if (m_dtCompletedStartDate.SelectedDate.HasValue && !m_dtCompletedEndDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CompletedDateTime.Date >= m_dtCompletedStartDate.SelectedDate.Value);
            }
            else if (!m_dtCompletedStartDate.SelectedDate.HasValue && m_dtCompletedEndDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CompletedDateTime.Date <= m_dtCompletedEndDate.SelectedDate.Value);
            }


            if(m_txtJobName.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.JobName.Contains(m_txtJobName.Text) || row.UserName.Contains(m_txtJobName.Text) || row.Email.Contains(m_txtJobName.Text) || row.CompanyName.Contains(m_txtJobName.Text));
            }

            if(m_chkRequestIsAsap.Checked)
            {
                oResults = oResults.Where(row => row.IsRushOrder == true);
            }

            List<string> oValues = new List<string>();
            if(m_cboRequestStatus.SelectedValue != string.Empty)
            {

                var oStatusPredicate = PredicateBuilder.False<fn_Producer_GetCompletedDashboardResult>();
                foreach (RadComboBoxItem oItem in m_cboRequestStatus.Items)
                {
                    CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                    if (chkStatus.Checked)
                    {                
                        switch (oItem.Text)
                        {
                            case "Processing":
                                oStatusPredicate = oStatusPredicate.Or(row => !row.IsSentToProduction);
                                break;
                            case "In Production":
                                oStatusPredicate = oStatusPredicate.Or(row => row.IsSentToProduction && row.CompletedDateTime <= new DateTime(1950, 1, 1));
                                break;
                            case "Completed":
                            default:
                                oStatusPredicate = oStatusPredicate.Or(row => row.CompletedDateTime > new DateTime(1950, 1, 1));
                                break;
                        }
                    }
                }

                oResults = oResults.Where(oStatusPredicate);
            }
            else
            {
                // Default filter of 'no filter' on show jobs marked as complete by QC
                oResults = oResults.Where(row => row.CompletedDateTime > new DateTime(1950, 1, 1));
            }

            oResults = oResults.Where(row => row.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Canceled));

            m_grdRequests.DataSource = oResults;
        }

        protected void OnDetailTableDataBindRequests(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem oDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            int iIARequestID = MemberProtect.Utility.ValidateInteger(oDataItem["IARequestID"].Text);

            e.DetailTableView.DataSource = DataAccess.IAJobs.Where(row => row.IARequestID == iIARequestID);
        }

        protected void OnItemDataBoundRequests(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if(oDataItem.OwnerTableView.Name == "Master")
                {
                    fn_Producer_GetCompletedDashboardResult jobResult = oDataItem.DataItem as fn_Producer_GetCompletedDashboardResult;
                    
                    if(oDataItem["IsRushOrder"].Text == "True")
                    {
                        oDataItem["IsRushOrder"].Style["color"] = "red";
                        oDataItem["IsRushOrder"].Text = "ASAP";
                    }
                    else
                    {
                        oDataItem["IsRushOrder"].Text = string.Empty;
                    }

                    // if needs estimate status
                    if (jobResult.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate))
                    {
                        oDataItem["IsRushOrder"].CssClass = "needsestimate";
                        oDataItem["IsRushOrder"].ToolTip = "Submitted & Needs Estimate";
                    }

                    // if on hold awaiting estimate status
                    if (jobResult.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval))
                    {
                        oDataItem["IsRushOrder"].ToolTip = "On Hold Awaiting Estimate Approval";
                    }

                    // if submitted & approved status
                    if (jobResult.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Submitted))
                    {
                        oDataItem["IsRushOrder"].CssClass = "approved";
                        oDataItem["IsRushOrder"].ToolTip = "Submitted & Approved";
                    }

                    if (jobResult.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.ReCutRequested))
                    {
                        oDataItem["IsRushOrder"].CssClass = "recut";
                        oDataItem["IsRushOrder"].Style["color"] = "white";
                        oDataItem["IsRushOrder"].Text = "RECUT";
                        oDataItem["IsRushOrder"].ToolTip = "Recut Requested";
                    }

                    if (oDataItem["ProductionDateTime"].Text == "1/01/1950 at 12:00 AM" || oDataItem["ProductionDateTime"].Text == "1/01/1900 at 12:00 AM")
                    {
                        oDataItem["ProductionDateTime"].Text = "N/A";
                    }

                    if (oDataItem["CompletedDateTime"].Text == "1/01/1950 at 12:00 AM" || oDataItem["CompletedDateTime"].Text == "1/01/1900 at 12:00 AM")
                    {
                        oDataItem["CompletedDateTime"].Text = "N/A";
                    }
                }
            }
        }

        protected void OnSortCommandRequests(object source, GridSortCommandEventArgs e)
        {
            ApplicationContext.SaveGridSettings("Staff Completed", e);
        }
        #endregion

        #region Private Methods
        private void ProcessLanguages()
        {
            m_cboRequestLanguage.SelectedValue = GetLabelValues("Languages");
            m_cboRequestLanguage.Text = GetLabelText("Languages");
        }

        private string GetLabelValues(string sName)
        {
            RadComboBox oComboBox = null;
            if(sName == "Languages")
            {
                oComboBox = m_cboRequestLanguage;
            }

            string sValues = string.Empty;

            List<string> oValues = new List<string>();
            foreach(RadComboBoxItem oItem in oComboBox.Items)
            {
                CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                if(chkStatus.Checked)
                {
                    sValues += string.Format("{0},", oItem.Value);
                }
            }
            sValues = sValues.TrimEnd(",".ToCharArray());

            return sValues;
        }

        private string GetLabelText(string sName)
        {
            RadComboBox oComboBox = null;
            if(sName == "Languages")
            {
                oComboBox = m_cboRequestLanguage;
            }

            string sText = string.Empty;

            foreach(RadComboBoxItem oItem in oComboBox.Items)
            {
                CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;
                if(chkStatus.Checked)
                {
                    sText += oItem.Text + ",";
                }
            }
            sText = sText.TrimEnd(",".ToCharArray());

            return sText;
        }
        #endregion

        #region Virtual Methods
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