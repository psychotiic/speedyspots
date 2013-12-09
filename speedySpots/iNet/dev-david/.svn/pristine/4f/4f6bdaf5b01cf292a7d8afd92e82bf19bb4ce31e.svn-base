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
using System.Text;

namespace SpeedySpots.Controls
{
   using Business;

   public partial class StaffDashboardRequests : SiteBaseControl
    {
        private StringBuilder _sb;
        private ss_Requests_DashboardResult _requestItem;
        private StaffRequestDashboardQuery.OrderByOptions _orderBy = StaffRequestDashboardQuery.OrderByOptions.IARequestID;
        private string _sortClass = "rgSortedDESC";

        public StaffRequestDashboardQuery Query { get; set; }

        public void BindData()
        {
            if (Query != null)
            {
                LoadDashboard();
            }
            else
            {
                throw new ArgumentNullException("Query");
            }
        }

        private void LoadDashboard()
        {
            _orderBy = Query.OrderBy;
            _sortClass = Query.OrderByDESC ? "rgSortedDESC" : "rgSortedASC";           

            IList<ss_Requests_DashboardResult> requests = RequestsService.GetRequestDashboardForStaffUser(Query, DataAccess);
            litNumberOfRecords.Text = (requests.Count > 0) ? requests[0].RecordCount.ToString() : "0";

            rptDashboard.DataSource = requests;
            rptDashboard.DataBind();
        }

        // This is ALL building in code because I'm trying to keep it as small as possible (NO silly IDs and such)
        // because this will be mostly transfering over the wire via jquery
        protected void rptDashboard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                bool isAlternatingRow = (e.Item.ItemType == ListItemType.AlternatingItem);
                _requestItem = (ss_Requests_DashboardResult)e.Item.DataItem;
                Literal litOutput = (Literal)e.Item.FindControl("litOutput");

                _sb = new StringBuilder();

                GetTRTag(isAlternatingRow);
                GetPriorityCell();
                GetRequestIDCell();
                GetRequestCompanyCell();
                GetRequestedByCell();
                GetRequestedDateCell();
                GetStatusCell();
                GetLabelsCell();

                _sb.Append("</tr>");
                litOutput.Text = _sb.ToString();
            }
        }

        private void GetTRTag(bool isAlternatingRow)
        {
            string mainClass = isAlternatingRow ? "rgAltRow" : "rgRow";
            string pastDueClass = _requestItem.IsPastDue.Value ? " pastdue" : string.Empty;
            string unownedClass = string.Empty;

            if (_requestItem.MPUserIDOwnedByStaff == Guid.Empty || (_requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Processing) && _requestItem.JobCount == 0))
            {
                unownedClass = " unownedrequest";
            }

            _sb.AppendFormat("<tr class=\"{0}{1}{2}\">", mainClass, pastDueClass, unownedClass);
        }

        private void GetPriorityCell()
        {
            string cellText = string.Empty;
            string cellToolTip = string.Empty;
            string cellClass = string.Empty;
            string cellTextColor = string.Empty;

            if (_requestItem.IsRushOrder)
            {
                cellTextColor = "color:red;";
                cellText = "ASAP";
            }

            // if needs estimate status
            if (_requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.NeedsEstimate))
            {
                cellClass = "needsestimate";
                cellToolTip = "Submitted & Needs Estimate";
            }

            // if on hold awaiting estimate status
            if (_requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.WaitingEstimateApproval))
            {
                cellToolTip = "On Hold Awaiting Estimate Approval";
            }

            // if submitted & approved status
            if (_requestItem.IARequestStatusID == ApplicationContext.GetRequestStatusID(RequestStatus.Submitted))
            {
                cellClass = "approved";
                cellToolTip = "Submitted & Approved";
            }

            if (_requestItem.RecutCount > 0)
            {
                cellClass = "recut";
                cellTextColor = "color:white;";
                cellText = "RECUT";
                cellToolTip = "Recut Requested";
            }

            cellClass += (_orderBy == StaffRequestDashboardQuery.OrderByOptions.IsRushOrder) ? " " + _sortClass : string.Empty;
            cellClass = cellClass.TrimStart(' ');

            //<td style="color:red;">ASAP</td>
            //<td title="Recut Requested" class="recut" style="color:white;">RECUT</td>
            _sb.Append("<td");
            _sb.Append(string.IsNullOrEmpty(cellToolTip) ? string.Empty : string.Format(" title=\"{0}\"", cellToolTip));
            _sb.Append(string.IsNullOrEmpty(cellClass) ? string.Empty : string.Format(" class=\"{0}\"", cellClass));
            _sb.Append(string.IsNullOrEmpty(cellTextColor) ? string.Empty : string.Format(" style=\"{0}\"", cellTextColor));
            _sb.AppendFormat(">{0}</td>", string.IsNullOrEmpty(cellText) ? string.Empty : cellText);
        }

        private void GetRequestIDCell()
        {
            string cellSortedBy = (_orderBy == StaffRequestDashboardQuery.OrderByOptions.IARequestID) ? string.Format(" class=\"{0}\"", _sortClass) : string.Empty;
            string tag = "<td{0}><a title=\"{1}\" href=\"{2}\">{1}</a></td>";

            _sb.AppendFormat(tag, cellSortedBy, _requestItem.IARequestID, GetRequestDeatilsURL());
        }

        private void GetRequestCompanyCell()
        {
            string cellSortedBy = (_orderBy == StaffRequestDashboardQuery.OrderByOptions.CompanyName) ? string.Format(" class=\"{0}\"", _sortClass) : string.Empty;
            _sb.AppendFormat("<td{0}><a href=\"{1}\">{2}</a></td>", cellSortedBy, GetRequestDeatilsURL(), _requestItem.CompanyName);
        }

        private void GetRequestedByCell()
        {
            string cellSortedBy = (_orderBy == StaffRequestDashboardQuery.OrderByOptions.UserName) ? string.Format(" class=\"{0}\"", _sortClass) : string.Empty;
            _sb.AppendFormat("<td{0}>{1}</td>", cellSortedBy, _requestItem.UserName);
        }

        private string GetRequestDeatilsURL()
        {
            string url = string.Format("~/create-job.aspx?s=r&rid={0}", _requestItem.IARequestID);
            url = ResolveUrl(url);

            return url;
        }

        private void GetRequestedDateCell()
        {
            string cellSortedBy = (_orderBy == StaffRequestDashboardQuery.OrderByOptions.CreatedDateTime) ? string.Format(" class=\"{0}\"", _sortClass) : string.Empty;
            _sb.AppendFormat("<td{0}>{1} at {2}</td>", cellSortedBy, _requestItem.CreatedDateTime.ToString("M/dd/yyyy"), _requestItem.CreatedDateTime.ToString("h:mm tt"));
        }

        private void GetStatusCell()
        {
            string cellSortedBy = (_orderBy == StaffRequestDashboardQuery.OrderByOptions.Status) ? string.Format(" class=\"{0}\"", _sortClass) : string.Empty;
            _sb.AppendFormat("<td{0}>{1}</td>", cellSortedBy, _requestItem.Status);
        }

        private void GetLabelsCell()
        {
            _sb.Append("<td>");

            if (!string.IsNullOrEmpty(_requestItem.AppliedLabelIDs))
            {
                string[] labelIds = _requestItem.AppliedLabelIDs.Split(',');

                foreach (string labelId in labelIds)
                {
                    IALabel label = Business.Services.LabelsService.GetLabel(int.Parse(labelId));
                    if (label != null)
                    {
                        _sb.AppendFormat("<span class=\"label\">{0}</span>", label.Text);
                    }
                }
            }

            _sb.Append("</td>");
        }
    }
}