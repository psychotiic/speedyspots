using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedySpots.Business.Models
{
    public class StaffRequestDashboardQuery
    {
        private List<int> _labels;
        private List<int> _status;

        public enum OrderByOptions
        {
            IARequestID,
            CompanyName,
			UserName,
            CreatedDateTime,
			IsRushOrder,
            Status
        }

        public StaffRequestDashboardQuery()
        {
            RequestNumber = string.Empty;
            RequestedBy = string.Empty;
            Lanugages = new List<string>();
            _status = GetDefaultStatus();
            _labels = new List<int>();
            LabelsShowOnlyUnlabeled = false;
            OrderBy = OrderByOptions.IARequestID;
            PageSize = 50;
            PageNumber = 1;
        }

        public string RequestNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestedBy { get; set; }
        public List<string> Lanugages { get; set; }
        public bool RequestASAP { get; set; }
        public bool LabelsShowOnlyUnlabeled { get; set; }
        public OrderByOptions OrderBy { get; set; }
        public bool OrderByDESC { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string LanugagesAsStringList 
        {
            get
            {
                string returnValue = string.Empty;
                foreach (string value in Lanugages)
                {
                    returnValue += string.Format("{0},", value);
                }

                return returnValue.TrimEnd(',');
            }
        }

        public string StatusAsStringList
        {
            get
            {
                string returnValue = string.Empty;
                foreach (int value in Status)
                {
                    returnValue += string.Format("{0},", value);
                }

                return returnValue.TrimEnd(',');
            }
        }

        public string LabelsAsStringList
        {
            get
            {
                string returnValue = string.Empty;
                foreach (int value in Labels)
                {
                    returnValue += string.Format("{0},", value);
                }

                return returnValue.TrimEnd(',');
            }
        }

        public string OrderBy_ForQuery
        {
            get
            {
                if (OrderByDESC)
                {
                    return OrderBy.ToString() + " DESC";
                }

                return OrderBy.ToString();
            }
        }

        /// <summary>
        /// Sets the order by for strings like "IARequestID DESC" or "RequestNumber ASC", DESC and ASC must be capatialized
        /// </summary>
        /// <param name="orderBy"></param>
        public void SetOrderBy(string orderBy)
        {
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.IndexOf("DESC") > 0)
                {
                    this.OrderByDESC = true;
                    orderBy = orderBy.Replace(" DESC", string.Empty);
                }
                else if (orderBy.IndexOf("ASC") > 0)
                {
                    this.OrderByDESC = false;
                    orderBy = orderBy.Replace(" ASC", string.Empty);
                }

                try
                {
                    this.OrderBy = (StaffRequestDashboardQuery.OrderByOptions)Enum.Parse(typeof(StaffRequestDashboardQuery.OrderByOptions), orderBy, true);
                }
                catch
                {
                    this.OrderBy = StaffRequestDashboardQuery.OrderByOptions.IARequestID;
                    this.OrderByDESC = true;
                }
            }
            else
            {
                this.OrderBy = StaffRequestDashboardQuery.OrderByOptions.IARequestID;
                this.OrderByDESC = true;
            }
        }

        /// <summary>
        /// List of labels applied, to set please use the SetLabels method
        /// </summary>
        public List<int> Labels
        {
            get
            {
                return _labels;
            }
        }

        /// <summary>
        /// If a -1 is past we'll clear the list cause it means "all" labels, if -2 is passed the list will be cleared
        /// and the LabelsShowOnlyUnlabeled property will be set to true
        /// </summary>
        /// <param name="labels"></param>
        public void SetLabels(List<int> labels)
        {
            if (labels.Contains(-1))
            {
                //Then we really want ALL labels, so clear out the list
                labels.Clear();
                labels.Add(-1);
            }

            if (labels.Contains(-2))
            {
                //Then we really want UNLABELD requests, so clear out the list
                labels.Clear();
                labels.Add(-2);
                LabelsShowOnlyUnlabeled = true;
            }

            _labels = labels;
        }

        public string GetLabelsForQuery()
        {
            string returnValue = string.Empty;
            List<int> labelValues = Labels;

            if (labelValues.Contains(-1))
            {
                returnValue = string.Empty;
            }
            else if (labelValues.Contains(-2))
            {
                LabelsShowOnlyUnlabeled = true;
                returnValue = string.Empty;
            }
            else
            {
                foreach (int value in labelValues)
                {
                    returnValue += string.Format("{0},", value);
                }
            }            

            return returnValue.TrimEnd(',');
        }

        public List<int> Status
        {
            get
            {
                return _status;
            }
        }

        /// <summary>
        /// Sets the status options, if an empty list is passed the status will be populated with the default status options
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(List<int> status)
        {
            if (status.Count <= 0)
            {
                status = GetDefaultStatus();
            }
            else if(status.Contains(0))
            {
                status.Remove(0);
            }

            _status = status;
        }

        private List<int> GetDefaultStatus()
        {
            List<int> status = new List<int>();
            status.Add((int)RequestStatus.NeedsEstimate);
            status.Add((int)RequestStatus.WaitingEstimateApproval);
            status.Add((int)RequestStatus.Submitted);
            status.Add((int)RequestStatus.Processing);

            return status;
        }
    }
}