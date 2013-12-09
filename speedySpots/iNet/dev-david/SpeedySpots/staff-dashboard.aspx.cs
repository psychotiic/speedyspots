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
using System.Transactions;

namespace SpeedySpots
{
   using Business;
   using JobStatus = Business.JobStatus;
   using SpotStatus = Business.SpotStatus;

   public partial class staff_dashboard : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // User must be a producer in order to visit this page
            if(!ApplicationContext.IsStaff)
            {
                Response.Redirect("~/Default.aspx");
            }

            // Enable whos on chat button to show on this page
            ((Site)Master).IsWhosOnInTrackOnlyMode = false;

            if(!IsPostBack)
            {
                m_divRequests.Visible = true;
                m_divInProduction.Visible = false;

                // Load status filter options
                m_cboRequestStatus.DataSource = DataAccess.fn_Site_GetRequestStatuses().OrderBy(row => row.SortOrder);
                m_cboRequestStatus.DataValueField = "IARequestStatusID";
                m_cboRequestStatus.DataTextField = "Name";
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

                m_cboInProductionLanguage.DataSource = oLanguages;
                m_cboInProductionLanguage.DataValueField = "Value";
                m_cboInProductionLanguage.DataTextField = "Value";
                m_cboInProductionLanguage.DataBind();

                m_cboJobStatus.DataValueField = "IAJobStatusID";
                m_cboJobStatus.DataTextField = "Name";
                m_cboJobStatus.DataSource = DataAccess.IAJobStatus.OrderBy(row => row.Name);
                m_cboJobStatus.DataBind();

                // Load status filter options
                m_cboLabels.DataSource = Business.Services.LabelsService.GetLabelsForFilterDropDown(DataAccess);
                m_cboLabels.DataValueField = "IALabelID";
                m_cboLabels.DataTextField = "Text";
                m_cboLabels.DataBind();

                m_cboTalent.DataSource = Business.Services.TalentsService.GetAllTalents(DataAccess);
                m_cboTalent.DataTextField = "FullName";
                m_cboTalent.DataValueField = "MPUserID";
                m_cboTalent.DataBind();

                m_cboTalent.Items.Insert(0, new RadComboBoxItem("-- All --", ""));
                m_cboTalent.SelectedIndex = 0;

                string sFilter = "requests";
                string sFilterString = string.Empty;
                if(Request.QueryString["filter"] != null)
                {
                    sFilter = Request.QueryString["filter"];
                }

                if(sFilter == "inproduction")
                {
                    ApplicationContext.LoadGridSettings("Staff Production", m_grdInProduction, out sFilterString);

                    m_divRequests.Visible = false;
                    m_divInProduction.Visible = true;

                    if(sFilterString != string.Empty)
                    {
                        string[] sFilters = sFilterString.Split(ApplicationContext.Separator.ToCharArray());
                        foreach(string sFilterChunk in sFilters)
                        {
                            string[] sFilterParts = sFilterChunk.Split(ApplicationContext.SubSeparator.ToCharArray());
                            string sName = sFilterParts[0];
                            string sValue = sFilterParts[1];

                            if(sName == "Job Name")
                            {
                                m_txtJobName.Text = sValue;
                            }
                            else if(sName == "Due Date/Time")
                            {
                                DateTime oDateTime = new DateTime();
                                DateTime.TryParse(sValue, out oDateTime);

                                if(oDateTime.Year == 1)
                                {
                                    m_dtOrderDate.SelectedDate = null;
                                }
                                else
                                {
                                    m_dtOrderDate.SelectedDate = oDateTime;
                                }
                            }
                            else if(sName == "ASAP")
                            {
                                m_chkInProductionIsAsap.Checked = MemberProtect.Utility.YesNoToBool(sValue);
                            }
                            else if(sName == "Job ID")
                            {
                                m_txtJobID.Text = sValue;
                            }
                            else if(sName == "Status")
                            {
                                m_cboJobStatus.SelectedValue = sValue;

                                string sText = string.Empty;
                                string sValues = string.Empty;
                                string[] oValues = m_cboJobStatus.SelectedValue.Split(",".ToCharArray());
                                foreach(RadComboBoxItem oItem in m_cboJobStatus.Items)
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

                                    m_cboJobStatus.Text = sText;
                                    m_cboJobStatus.SelectedValue = sValues;
                                }
                            }
                            else if(sName == "Languages")
                            {
                                m_cboInProductionLanguage.SelectedValue = sValue;

                                string[] oValues = m_cboInProductionLanguage.SelectedValue.Split(",".ToCharArray());
                                foreach(RadComboBoxItem oItem in m_cboInProductionLanguage.Items)
                                {
                                    if(oValues.Contains(oItem.Value))
                                    {
                                        CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;
                                        chkStatus.Checked = true;
                                    }
                                }

                                ProcessProductionLanguages();
                            }
                            else if(sName == "Talent")
                            {
                                m_cboTalent.SelectedValue = sValue;
                            }
                        }
                    }
                    else
                    {
                        // Defaults
                        foreach(RadComboBoxItem oItem in m_cboInProductionLanguage.Items)
                        {
                            CheckBox chkLanguage = oItem.FindControl("m_chkStatus") as CheckBox;
                            chkLanguage.Checked = true;
                        }

                        ProcessProductionLanguages();
                    }

                    OnFilterInProduction(this, new EventArgs());
                }
                else
                {
                    ApplicationContext.LoadGridSettings("Staff Requests", m_grdRequests, out sFilterString);

                    // Mac does not want the details grid showing for the Requests tab, so we remove it in that case
                    m_grdRequests.MasterTableView.DetailTables.RemoveAt(0);
                    m_grdRequests.MasterTableView.Columns.FindByUniqueName("Expand").Visible = false;

                    if(sFilterString != string.Empty)
                    {
                        string[] sFilters = sFilterString.Split(ApplicationContext.Separator.ToCharArray());
                        foreach(string sFilterChunk in sFilters)
                        {
                            string[] sFilterParts = sFilterChunk.Split(ApplicationContext.SubSeparator.ToCharArray());
                            string sName = sFilterParts[0];
                            string sValue = sFilterParts[1];

                            if(sName == "Request Number")
                            {
                                m_txtRequestNumber.Text = sValue;
                            }
                            else if(sName == "Request Date")
                            {
                                DateTime oDateTime = new DateTime();
                                DateTime.TryParse(sValue, out oDateTime);

                                if(oDateTime.Year == 1)
                                {
                                    m_dtCreatedDate.SelectedDate = null;
                                }
                                else
                                {
                                    m_dtCreatedDate.SelectedDate = oDateTime;
                                }
                            }
                            else if(sName == "Requested By")
                            {
                                m_txtRequestedBy.Text = sValue;
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
                            else if(sName == "Labels")
                            {
                                m_cboLabels.SelectedValue = sValue;

                                string[] oValues = m_cboLabels.SelectedValue.Split(",".ToCharArray());
                                foreach(RadComboBoxItem oItem in m_cboLabels.Items)
                                {
                                    if(oValues.Contains(oItem.Value))
                                    {
                                        CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;
                                        chkStatus.Checked = true;
                                    }
                                }

                                ProcessLabels();
                            }
                        }
                    }
                    else
                    {
                        // Defaults
                        if(sFilter == "requests")
                        {
                            foreach(RadComboBoxItem oItem in m_cboLabels.Items)
                            {
                                if(oItem.Text == "-- All --")
                                {
                                    CheckBox chkAll = oItem.FindControl("m_chkStatus") as CheckBox;
                                    chkAll.Checked = true;
                                }
                            }
                            
                            foreach(RadComboBoxItem oItem in m_cboRequestLanguage.Items)
                            {
                                CheckBox chkLanguage = oItem.FindControl("m_chkStatus") as CheckBox;
                                chkLanguage.Checked = true;
                            }
                            
                            ProcessLanguages();
                        }
                    }

                    OnFilterRequests(this, new EventArgs());
                }
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

                ProcessLabels();
                ProcessLanguages();

                oFilterString.Append(string.Format("Request Number{0}{1}{2}", ApplicationContext.SubSeparator, m_txtRequestNumber.Text, ApplicationContext.Separator));
                oFilterString.Append(string.Format("Request Date{0}{1}{2}", ApplicationContext.SubSeparator, (m_dtCreatedDate.SelectedDate == null ? string.Empty : m_dtCreatedDate.SelectedDate.Value.ToString("g")), ApplicationContext.Separator));
                oFilterString.Append(string.Format("Requested By{0}{1}{2}", ApplicationContext.SubSeparator, m_txtRequestedBy.Text, ApplicationContext.Separator));
                oFilterString.Append(string.Format("Status{0}{1}{2}", ApplicationContext.SubSeparator, m_cboRequestStatus.SelectedValue, ApplicationContext.Separator));
                oFilterString.Append(string.Format("Languages{0}{1}{2}", ApplicationContext.SubSeparator, m_cboRequestLanguage.SelectedValue, ApplicationContext.Separator));
                oFilterString.Append(string.Format("ASAP{0}{1}{2}", ApplicationContext.SubSeparator, MemberProtect.Utility.BoolToYesNo(m_chkRequestIsAsap.Checked), ApplicationContext.Separator));
                oFilterString.Append(string.Format("Labels{0}{1}{2}", ApplicationContext.SubSeparator, m_cboLabels.SelectedValue, ApplicationContext.Separator));

                // Remove last separator
                oFilterString.Remove(oFilterString.Length - 1, 1);

                ApplicationContext.SaveGridSettings("Staff Requests", oFilterString.ToString());
            }

            m_grdRequests.Rebind();
        }

        protected void OnNeedDataSourceRequests(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string sLabels = m_cboLabels.SelectedValue;
            if(ShowAll() || ShowUnlabeledOnly())
            {
                sLabels = string.Empty;
            }

            IQueryable<fn_Producer_GetDashboardResult> oResults = DataAccess.fn_Producer_GetDashboard(sLabels);

            if(ShowUnlabeledOnly())
            {
                oResults = oResults.Where(row => row.LabelCount == 0);
            }

            // Apply filters
            if(m_cboRequestLanguage.SelectedValue != string.Empty)
            {
                var oPredicate = PredicateBuilder.False<fn_Producer_GetDashboardResult>();

                string[] sLanguages = m_cboRequestLanguage.SelectedValue.Split(",".ToCharArray());
                foreach(string sLanguage in sLanguages)
                {
                    if(sLanguage == "Unspecified")
                    {
                        oPredicate = oPredicate.Or(row => row.JobCount == 0);
                    }
                    else if(sLanguage == "English")
                    {
                        oPredicate = oPredicate.Or(row => row.EnglishCount > 0);
                    }
                    else if(sLanguage == "Spanish")
                    {
                        oPredicate = oPredicate.Or(row => row.SpanishCount > 0);
                    }
                }

                oResults = oResults.Where(oPredicate);
            }

            if(m_txtRequestNumber.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.IARequestID == MemberProtect.Utility.ValidateInteger(m_txtRequestNumber.Text));
            }
                       
            if(m_dtCreatedDate.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date == m_dtCreatedDate.SelectedDate.Value);
            }

            if(m_txtRequestedBy.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.UserName.Contains(m_txtRequestedBy.Text) || row.Email.Contains(m_txtRequestedBy.Text) || row.CompanyName.Contains(m_txtRequestedBy.Text));
            }

            List<string> oValues = new List<string>();
            if(m_cboRequestStatus.SelectedValue != string.Empty)
            {
                foreach(RadComboBoxItem oItem in m_cboRequestStatus.Items)
                {
                    CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                    if(chkStatus.Checked)
                    {
                        oValues.Add(oItem.Value);
                    }
                }

                oResults = oResults.Where(row => oValues.Contains(row.IARequestStatusID.ToString()));
            }
            else
            {
                // Default filter of 'no filter' still shouldn't show 'Completed' or 'In Production' or 'Canceled' requests
                oResults = oResults.Where(row => row.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.InProduction) && row.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Completed) && row.IARequestStatusID != ApplicationContext.GetRequestStatusID(RequestStatus.Canceled));
            }
            
            if(m_chkRequestIsAsap.Checked)
            {
                oResults = oResults.Where(row => row.IsRushOrder == true);
            }

            using (var txn = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }
            ))
            {
                m_grdRequests.DataSource = oResults;
            }

            
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
                fn_Producer_GetDashboardResult requestItem = oDataItem.DataItem as fn_Producer_GetDashboardResult;

                if(oDataItem.OwnerTableView.Name == "Master")
                {
                    oDataItem["Expand"].Text = string.Empty;

                    int iIARequestID = MemberProtect.Utility.ValidateInteger(oDataItem["IARequestID"].Text);
                    
                    if(requestItem.MPUserIDOwnedByStaff == Guid.Empty)
                    {
                        oDataItem["IsRushOrder"].CssClass = "unownedrequest";
                        oDataItem["RequestNumber"].CssClass = "unownedrequest";
                        oDataItem["CompanyName"].CssClass = "unownedrequest";
                        oDataItem["UserName"].CssClass = "unownedrequest";
                        oDataItem["CreatedDateTime"].CssClass = "unownedrequest";
                        oDataItem["Status"].CssClass = "unownedrequest";
                        oDataItem["Labels"].CssClass = "unownedrequest";
                    }

                    if(requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Processing) && requestItem.JobCount == 0)
                    {
                        oDataItem["IsRushOrder"].CssClass = "unownedrequest";
                        oDataItem["RequestNumber"].CssClass = "unownedrequest";
                        oDataItem["CompanyName"].CssClass = "unownedrequest";
                        oDataItem["UserName"].CssClass = "unownedrequest";
                        oDataItem["CreatedDateTime"].CssClass = "unownedrequest";
                        oDataItem["Status"].CssClass = "unownedrequest";
                        oDataItem["Labels"].CssClass = "unownedrequest";
                    }
                    

                    if(oDataItem["IsRushOrder"].Text == "True")
                    {
                        oDataItem["IsRushOrder"].Style["color"] = "red";
                        oDataItem["IsRushOrder"].Text = "ASAP";
                    }
                    else
                    {
                        oDataItem["IsRushOrder"].Text = string.Empty;
                    }

                    if(oDataItem["IsPastDue"].Text == "1")
                    {
                        if(!oDataItem.CssClass.Contains("pastdue"))
                        {
                            string sBaseCSS = (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ? "rgRow" : "rgAltRow");
                            oDataItem.CssClass = sBaseCSS + " pastdue";
                        }
                    }

                    // if needs estimate status
                    if(requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate))
                    {
                        oDataItem["IsRushOrder"].CssClass = "needsestimate";
                        oDataItem["IsRushOrder"].ToolTip = "Submitted & Needs Estimate";
                    }

                    // if on hold awaiting estimate status
                    if(requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval))
                    {
                        oDataItem["IsRushOrder"].ToolTip = "On Hold Awaiting Estimate Approval";
                    }

                    // if submitted & approved status
                    if(requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Submitted))
                    {
                        oDataItem["IsRushOrder"].CssClass = "approved";
                        oDataItem["IsRushOrder"].ToolTip = "Submitted & Approved";
                    }

                    if(requestItem.RecutCount > 0)
                    {
                        oDataItem["IsRushOrder"].CssClass = "recut";
                        oDataItem["IsRushOrder"].Style["color"] = "white";
                        oDataItem["IsRushOrder"].Text = "RECUT";
                        oDataItem["IsRushOrder"].ToolTip = "Recut Requested";
                    }


                    // Load labels
                    string sLabels = string.Empty;

                    if(!string.IsNullOrEmpty(requestItem.AppliedLabelIDs))
                    {
                        string[] labelIds = requestItem.AppliedLabelIDs.Split(',');

                        foreach (string labelId in labelIds)
                        {
                            IALabel label = Business.Services.LabelsService.GetLabel(int.Parse(labelId));
                            if (label != null)
                            {
                                sLabels += string.Format("<span class=\"label\">{0}</span>", label.Text);
                            }
                        }
                    }
                    
                    oDataItem["Labels"].Text = sLabels;
                }
            }
        }

        protected void OnItemCreatedRequests(object source, GridItemEventArgs e)
        {
            if(e.Item is GridHeaderItem) 
            {
                if(e.Item.OwnerTableView.Name == "Master")
                {
                    GridHeaderItem oHeaderItem = (GridHeaderItem)e.Item;

                    LinkButton oButton = new LinkButton();
                    oButton.ID = "ButtonExpandCollapse";
                    oButton.CssClass = "headerButton";
                    oButton.Click += new EventHandler(OnExpandRequests);
                    oButton.EnableViewState = true;
                    if(m_grdRequests.MasterTableView.HierarchyDefaultExpanded)
                    {
                        oButton.Text = "-";
                    }
                    else
                    {
                        oButton.Text = "+";
                    }

                    oHeaderItem["Expand"].Controls.Add(oButton);
                }
            }
        }

        void OnExpandRequests(object sender, EventArgs e) 
        { 
            m_grdRequests.MasterTableView.HierarchyDefaultExpanded = !m_grdRequests.MasterTableView.HierarchyDefaultExpanded;
            m_grdRequests.Rebind(); 
        }

        protected void OnSortCommandRequests(object source, GridSortCommandEventArgs e)
        {
            ApplicationContext.SaveGridSettings("Staff Requests", e);
        }
        #endregion

        #region Private Methods
        private void ProcessLabels()
        {
            bool bShowAll = false;
            bool bShowUnlabeledOnly = false;
            foreach(RadComboBoxItem oItem in m_cboLabels.Items)
            {
                CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                if(chkStatus.Checked)
                {
                    if(oItem.Text == "-- All --")
                    {
                        bShowAll = true;
                        bShowUnlabeledOnly = false;
                    }
                    else if(oItem.Text == "-- Unlabeled --")
                    {
                        if(!bShowAll)
                        {
                            bShowUnlabeledOnly = true;
                        }
                    }
                }
            }

            // If 'All' is selected, unselect everything else
            if(bShowAll)
            {
                foreach(RadComboBoxItem oItem in m_cboLabels.Items)
                {
                    CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                    if(chkStatus.Checked)
                    {
                        if(oItem.Text != "-- All --")
                        {
                            chkStatus.Checked = false;
                        }
                    }
                }
            }

            // If 'Unlabeled' is selected, unselect everything else
            if(bShowUnlabeledOnly)
            {
                foreach(RadComboBoxItem oItem in m_cboLabels.Items)
                {
                    CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                    if(chkStatus.Checked)
                    {
                        if(oItem.Text != "-- Unlabeled --")
                        {
                            chkStatus.Checked = false;
                        }
                    }
                }
            }

            m_cboLabels.SelectedValue = GetLabelValues("Labels");
            m_cboLabels.Text = GetLabelText("Labels");
        }

        private void ProcessLanguages()
        {
            m_cboRequestLanguage.SelectedValue = GetLabelValues("Languages");
            m_cboRequestLanguage.Text = GetLabelText("Languages");
        }

        private void ProcessProductionLanguages()
        {
            m_cboInProductionLanguage.SelectedValue = GetLabelValues("ProductionLanguages");
            m_cboInProductionLanguage.Text = GetLabelText("ProductionLanguages");
        }

        private string GetLabelValues(string sName)
        {
            RadComboBox oComboBox = null;
            if(sName == "Languages")
            {
                oComboBox = m_cboRequestLanguage;
            }
            else if(sName == "ProductionLanguages")
            {
                oComboBox = m_cboInProductionLanguage;
            }
            else if(sName == "Labels")
            {
                oComboBox = m_cboLabels;
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
            else if(sName == "ProductionLanguages")
            {
                oComboBox = m_cboInProductionLanguage;
            }
            else if(sName == "Labels")
            {
                oComboBox = m_cboLabels;
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

        private bool ShowAll()
        {
            bool bResult = false;

            foreach(RadComboBoxItem oItem in m_cboLabels.Items)
            {
                CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                if(oItem.Text == "-- All --")
                {
                    if(chkStatus.Checked)
                    {
                        bResult = true;
                        break;
                    }
                }
            }

            return bResult;
        }

        private bool ShowUnlabeledOnly()
        {
            bool bResult = false;

            foreach(RadComboBoxItem oItem in m_cboLabels.Items)
            {
                CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                if(oItem.Text == "-- Unlabeled --")
                {
                    if(chkStatus.Checked)
                    {
                        bResult = true;
                        break;
                    }
                }
            }

            return bResult;
        }
        #endregion

        #region Grid (In Production)
        protected void OnFilterInProduction(object sender, EventArgs e)
        {
            if(m_cboJobStatus.SelectedValue == string.Empty)
            {
                // Due to custom text, we need to build the value string manually
                string[] oText = m_cboJobStatus.Text.Split(",".ToCharArray());
                foreach(string sText in oText)
                {
                    RadComboBoxItem oItem = m_cboJobStatus.FindItemByText(sText);
                    if(oItem != null)
                    {
                        m_cboJobStatus.SelectedValue += oItem.Value + ",";
                    }
                }
            }

            m_cboJobStatus.SelectedValue.TrimEnd(",".ToCharArray());
            m_cboInProductionLanguage.SelectedValue.TrimEnd(",".ToCharArray());

            ProcessProductionLanguages();

            StringBuilder oFilterString = new StringBuilder();
            oFilterString.Append(string.Format("Job Name{0}{1}{2}", ApplicationContext.SubSeparator, m_txtJobName.Text, ApplicationContext.Separator));
            oFilterString.Append(string.Format("Due Date/Time{0}{1}{2}", ApplicationContext.SubSeparator, (m_dtOrderDate.SelectedDate == null ? string.Empty : m_dtOrderDate.SelectedDate.Value.ToString("g")), ApplicationContext.Separator));
            oFilterString.Append(string.Format("ASAP{0}{1}{2}", ApplicationContext.SubSeparator, MemberProtect.Utility.BoolToYesNo(m_chkInProductionIsAsap.Checked), ApplicationContext.Separator));
            oFilterString.Append(string.Format("Job ID{0}{1}{2}", ApplicationContext.SubSeparator, m_txtJobID.Text, ApplicationContext.Separator));
            oFilterString.Append(string.Format("Status{0}{1}{2}", ApplicationContext.SubSeparator, m_cboJobStatus.SelectedValue, ApplicationContext.Separator));
            oFilterString.Append(string.Format("Languages{0}{1}{2}", ApplicationContext.SubSeparator, m_cboInProductionLanguage.SelectedValue, ApplicationContext.Separator));
            oFilterString.Append(string.Format("Talent{0}{1}{2}", ApplicationContext.SubSeparator, m_cboTalent.SelectedValue, ApplicationContext.Separator));                
                
            // Remove last separator
            oFilterString.Remove(oFilterString.Length - 1, 1);

            ApplicationContext.SaveGridSettings("Staff Production", oFilterString.ToString());

            m_grdInProduction.Rebind();
        }

        protected void OnNeedDataSourceInProduction(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if(!e.IsFromDetailTable)
            {
                IQueryable<fn_QualityControl_GetDashboardJobsResult> oResults = null;

                if(m_cboTalent.SelectedValue == string.Empty)
                {
                    oResults = DataAccess.fn_QualityControl_GetDashboardJobs(Guid.Empty);
                }
                else
                {
                    oResults = DataAccess.fn_QualityControl_GetDashboardJobs(MemberProtect.Utility.ValidateGuid(m_cboTalent.SelectedValue));
                }

                // Default status until some other status is selected
                if(m_cboJobStatus.SelectedValue == string.Empty)
                {
                    oResults = oResults.Where(row => row.SpotsNotOnHold > 0 && row.IAJobStatusID != ApplicationContext.GetJobStatusID(JobStatus.Complete));
                }
                else
                {
                    List<string> oValues = new List<string>();
                    foreach(RadComboBoxItem oItem in m_cboJobStatus.Items)
                    {
                        CheckBox chkStatus = oItem.FindControl("m_chkStatus") as CheckBox;

                        if(chkStatus.Checked)
                        {
                            oValues.Add(oItem.Value);
                        }
                    }

                    oResults = oResults.Where(row => oValues.Contains(row.IAJobStatusID.ToString()));
                }

                // Apply filters
                if(m_cboInProductionLanguage.SelectedValue != string.Empty)
                {
                    var oPredicate = PredicateBuilder.False<fn_QualityControl_GetDashboardJobsResult>();

                    string[] sLanguages = m_cboInProductionLanguage.SelectedValue.Split(",".ToCharArray());
                    foreach(string sLanguage in sLanguages)
                    {
                        if(sLanguage == "English")
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

                if(m_txtJobName.Text != string.Empty)
                {
                    oResults = oResults.Where(row => row.JobName.Contains(m_txtJobName.Text));
                }

                if(m_txtJobID.Text != string.Empty)
                {
                    oResults = oResults.Where(row => row.DisplayJobID.Contains(m_txtJobID.Text));
                }

                if(m_dtOrderDate.SelectedDate != null)
                {
                    oResults = oResults.Where(row => row.DueDateTime.Date == m_dtOrderDate.SelectedDate);
                }

                if(m_chkInProductionIsAsap.Checked)
                {
                    oResults = oResults.Where(row => row.IsAsap == true);
                }

                m_grdInProduction.DataSource = oResults;
            }
        }

        protected void OnDetailTableDataBindInProduction(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem oDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            int iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);

            e.DetailTableView.DataSource = DataAccess.fn_QualityControl_GetDashboardSpots(iIAJobID).Where(row => row.IASpotStatusID != ApplicationContext.GetSpotStatusID(SpotStatus.OnHold));
        }

        protected void OnItemDataBoundInProduction(object sender, GridItemEventArgs e)
        {
            if(e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if(oDataItem.OwnerTableView.Name == "Master")
                {
                    oDataItem["Expand"].Text = string.Empty;
                    fn_QualityControl_GetDashboardJobsResult jobsResult = oDataItem.DataItem as fn_QualityControl_GetDashboardJobsResult;

                    int iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                                       
                    // Highlight link if the job has any additional production
                    if (jobsResult.IsConvert || jobsResult.IsMusic || jobsResult.IsProduction || jobsResult.IsSFX)
                    {
                        HyperLink oButton = oDataItem["JobName"].Controls[0] as HyperLink;
                        oButton.CssClass = "addtlProduction";
                    }

                    oDataItem["JobID"].Text = jobsResult.DisplayJobID;

                    if (jobsResult.IsAsap || jobsResult.AsapSpotCount > 0)
                    {
                        if (oDataItem["Priority"].Text == "True")
                        {
                            oDataItem["Priority"].Style["color"] = "red";
                            oDataItem["Priority"].Text = "ASAP";
                        }
                        else
                        {
                            oDataItem["Priority"].Text = string.Empty;
                        }
                    }
                    else
                    {
                        oDataItem["Priority"].Text = string.Empty;
                    }

                    if (oDataItem["IsPastDue"].Text == "1")
                    {
                        if (!oDataItem.CssClass.Contains("pastdue"))
                        {
                            string sBaseCSS = (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item ? "rgRow" : "rgAltRow");
                            oDataItem.CssClass = sBaseCSS + " pastdue";
                        }
                    }
                    
                }
                else if(oDataItem.OwnerTableView.Name == "Detail")
                {
                    if(oDataItem["Priority"].Text == "True")
                    {
                        oDataItem["Priority"].Style["color"] = "red";
                        oDataItem["Priority"].Text = "ASAP";
                    }
                    else
                    {
                        oDataItem["Priority"].Text = string.Empty;
                    }
                }
            }
        }

        protected void OnItemCreatedInProduction(object source, GridItemEventArgs e)
        {
            if(e.Item is GridHeaderItem)
            {
                if(e.Item.OwnerTableView.Name == "Master")
                {
                    GridHeaderItem oHeaderItem = (GridHeaderItem)e.Item;

                    LinkButton oButton = new LinkButton();
                    oButton.ID = "ButtonExpandCollapse";
                    oButton.CssClass = "headerButton";
                    oButton.Click += new EventHandler(OnExpandInProduction);
                    oButton.EnableViewState = true;
                    if(m_grdInProduction.MasterTableView.HierarchyDefaultExpanded)
                    {
                        oButton.Text = "-";
                    }
                    else
                    {
                        oButton.Text = "+";
                    }

                    oHeaderItem["Expand"].Controls.Add(oButton);
                }
            }
        }

        void OnExpandInProduction(object sender, EventArgs e)
        {
            m_grdInProduction.MasterTableView.HierarchyDefaultExpanded = !m_grdInProduction.MasterTableView.HierarchyDefaultExpanded;
            m_grdInProduction.Rebind();
        }

        protected void OnSortCommandInProduction(object source, GridSortCommandEventArgs e)
        {
            ApplicationContext.SaveGridSettings("Staff Production", e);
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
