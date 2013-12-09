using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeedySpots.Objects;
using SpeedySpots.DataAccess;
using Telerik.Web.UI;
using System.Text;
using LinqKit;

namespace SpeedySpots
{
   using Business;

   public partial class user_requests : SiteBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ApplicationContext.IsCustomer)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                LoadCoworkersForFilter();
                m_grdList.PageSize = ApplicationContext.GetGridPageSize();

                SetDefaultButton(m_dtCreatedDateStart, m_btnFilter);
                SetDefaultButton(m_dtCreatedDateEnd, m_btnFilter);
                SetDefaultButton(m_txtJobTitle, m_btnFilter);
            }
        }

        private void LoadCoworkersForFilter()
        {
            var query = from c in DataAccess.IACustomerCoworkers
                        where c.MPUserID == MemberProtect.CurrentUser.UserID
                        join u in DataAccess.MPUserDatas on c.MPUserIDCoworker equals u.MPUserID
                        select new
                        {
                            MPUserID = c.MPUserIDCoworker,
                            Name = u.FirstName + " " + u.LastName
                        };

            string currentUsersName = string.Format("{0} {1}", MemberProtect.CurrentUser.GetDataItem("FirstName"), MemberProtect.CurrentUser.GetDataItem("LastName"));

            var coworkerList = new[] { new { MPUserID = MemberProtect.CurrentUser.UserID, Name = currentUsersName } }.Concat(query);

            m_cboRequestCoworker.DataSource = coworkerList.OrderBy(c => c.Name);
            m_cboRequestCoworker.DataValueField = "MPUserID";
            m_cboRequestCoworker.DataTextField = "Name";
            m_cboRequestCoworker.DataBind();
        }

        protected void cboRequestCoworkerItemDataBind(object sender, RadComboBoxItemEventArgs e)
        {
            CheckBox m_chkStatus = (CheckBox)e.Item.FindControl("m_chkStatus");
            m_chkStatus.Attributes.Add("data-userID", e.Item.Value);
        }

        #region Grid
        protected void OnFilterRequests(object sender, EventArgs e)
        {
            m_grdList.Rebind();
        }

        protected void OnNeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            IQueryable<fn_Customer_GetRequestsResult> oResults = DataAccess.fn_Customer_GetRequests(MemberProtect.CurrentUser.UserID);

            if (m_dtCreatedDateStart.SelectedDate.HasValue && m_dtCreatedDateEnd.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date >= m_dtCreatedDateStart.SelectedDate.Value && row.CreatedDateTime.Date <= m_dtCreatedDateEnd.SelectedDate.Value);
            }
            else if (m_dtCreatedDateStart.SelectedDate.HasValue && !m_dtCreatedDateEnd.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date >= m_dtCreatedDateStart.SelectedDate.Value);
            }
            else if (!m_dtCreatedDateStart.SelectedDate.HasValue && m_dtCreatedDateEnd.SelectedDate.HasValue)
            {
                oResults = oResults.Where(row => row.CreatedDateTime.Date <= m_dtCreatedDateEnd.SelectedDate.Value);
            }

            if (m_txtJobTitle.Text != string.Empty)
            {
                oResults = oResults.Where(row => row.Name.Contains(m_txtJobTitle.Text) || row.IARequestID.ToString().Contains(m_txtJobTitle.Text) || row.Username.Contains(m_txtJobTitle.Text));
            }

            if (m_cboRequestCoworker.SelectedValue != string.Empty)
            {
                var oPredicate = PredicateBuilder.False<fn_Customer_GetRequestsResult>();

                string[] sCoworkers = m_cboRequestCoworker.SelectedValue.Split(",".ToCharArray());
                foreach (string sCoworker in sCoworkers)
                {
                    Guid userID = new Guid(sCoworker);
                    oPredicate = oPredicate.Or(row => row.MPUserID == userID);
                }

                oResults = oResults.Where(oPredicate);
            }

            m_grdList.DataSource = oResults;
        }

        protected void OnDetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem oDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            int iIARequestID = MemberProtect.Utility.ValidateInteger(oDataItem["IARequestID"].Text);

            e.DetailTableView.DataSource = DataAccess.IAJobs.Where(row => row.IARequestID == iIARequestID);
        }

        protected void OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem oDataItem = e.Item as GridDataItem;

                if (oDataItem.OwnerTableView.Name == "Master")
                {
                    int iIAJobID = MemberProtect.Utility.ValidateInteger(oDataItem["IAJobID"].Text);
                    int iIARequestID = MemberProtect.Utility.ValidateInteger(oDataItem["IARequestID"].Text);

                    IARequest oIARequest = DataAccess.IARequests.SingleOrDefault(row => row.IARequestID == iIARequestID);
                    if (oIARequest != null)
                    {
                        if (!oIARequest.IsLocked)
                        {
                            HyperLink titleColumn = (HyperLink)oDataItem["Title"].Controls[0];
                            titleColumn.NavigateUrl = string.Format("~/edit-request.aspx?s=a&id={0}", oIARequest.IARequestID);

                            HyperLink requestColumn = (HyperLink)oDataItem["RequestNumber"].Controls[0];
                            requestColumn.NavigateUrl = string.Format("~/edit-request.aspx?s=a&id={0}", oIARequest.IARequestID);
                        }

                        if (iIAJobID > 0)
                        {
                            IAJob oIAJob = DataAccess.IAJobs.SingleOrDefault(row => row.IAJobID == iIAJobID);
                            if (oIAJob.IAJobStatusID == ApplicationContext.GetJobStatusID(JobStatus.Complete))
                            {
                                oDataItem["ExpectedDelivery"].Style["font-weight"] = "bold";
                                oDataItem["ExpectedDelivery"].Text = "Delivered";
                            }
                            else
                            {
                                oDataItem["ExpectedDelivery"].Text = string.Format("{0:M/dd/yyyy a\\t h:mm tt}", oIAJob.DueDateTime);
                            }
                        }
                        else
                        {
                            oDataItem["Title"].Controls.Clear();

                            oDataItem["Title"].Style["font-style"] = "italic";
                            oDataItem["Title"].Text = "To Be Determined";

                            oDataItem["ExpectedDelivery"].Style["font-style"] = "italic";
                            oDataItem["ExpectedDelivery"].Text = "To Be Determined";
                        }                       

                        // Load labels
                        string sLabels = string.Empty;

                        foreach (fn_Customer_GetRequestLabelsResult oResults in DataAccess.fn_Customer_GetRequestLabels(iIARequestID))
                        {
                            sLabels += string.Format("<span class=\"label\">{0}</span>", oResults.Text);
                        }
                        sLabels = sLabels.TrimEnd(" ,".ToCharArray());

                        oDataItem["Labels"].Text = sLabels;

                        if (oDataItem["Status"].Text == "Needs Estimate")
                        {
                            // We hide the 'Needs Estimate' from the customer and simplify it with 'Submitted'
                            oDataItem["Status"].Text = "Submitted";
                        }
                        else if (oDataItem["Status"].Text == "Waiting Estimate Approval")
                        {
                            // We hide the 'Estimate Approval' from the customer and simplify it with 'Waiting Estimate Approval'
                            oDataItem["Status"].Text = "Estimate Approval";
                        }
                        else if (oDataItem["Status"].Text == "In Production")
                        {
                            // We modify the 'In Production' from the customer and simplify it with 'Production'
                            oDataItem["Status"].Text = "Production";
                        }
                    }
                }
            }
        }

        protected void OnSortCommandRequests(object sender, GridSortCommandEventArgs e)
        {
            
        }

        #endregion

        #region Virtual Methods
        public override List<AccessControl> GetAccessControl()
        {
            List<AccessControl> oAccessControl = new List<AccessControl>();

            oAccessControl.Add(AccessControl.Customer);

            return oAccessControl;
        }
        #endregion
    }
}