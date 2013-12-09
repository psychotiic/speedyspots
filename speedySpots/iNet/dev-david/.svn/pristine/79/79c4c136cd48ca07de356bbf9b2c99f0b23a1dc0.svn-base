using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;
using System.Web;

namespace SpeedySpots.Business.Services
{
    public static class StaffDashboardRequestsService
    {
        private const string CacheKey = "StaffRequestDashboardQuery";

        /// <summary>
        /// Will pull the specified user's dashboard query from cache (if availale) or the DB and then cache it
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static StaffRequestDashboardQuery GetStaffRequestDashboardSettings(Guid userID, DataAccessDataContext context)
        {
            StaffRequestDashboardQuery query = new StaffRequestDashboardQuery();
            string cacheKey = string.Format("{0}-{1}", CacheKey, userID);

            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                query = GetRequestDashboardQueryFromDB(userID, context);
                CacheDashboardQuery(userID, query);
            }
            else
            {
                query = (StaffRequestDashboardQuery)HttpContext.Current.Cache[cacheKey];
            }

            return query;
        }

        /// <summary>
        /// Pull the user's dashboard perferences from the DB
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static StaffRequestDashboardQuery GetRequestDashboardQueryFromDB(Guid userID, DataAccessDataContext context)
        {
            StaffRequestDashboardQuery query = new StaffRequestDashboardQuery();
            IAUserSettingsGrid oIAUserSettingsGrid = context.IAUserSettingsGrids.SingleOrDefault(row => row.MPUserID == userID && row.Name == "Staff Requests");
            if (oIAUserSettingsGrid != null)
            {
                query = ParseRequestsDashboardQuery(oIAUserSettingsGrid.Filters, oIAUserSettingsGrid.SortExpression);
            }

            return query;
        }

        private static StaffRequestDashboardQuery ParseRequestsDashboardQuery(string filterString, string orderBy)
        {
            StaffRequestDashboardQuery query = new StaffRequestDashboardQuery();

            query.SetOrderBy(orderBy);

            if (!string.IsNullOrEmpty(filterString))
            {
                string[] sFilters = filterString.Split(char.ConvertFromUtf32(200).ToCharArray());
                foreach (string sFilterChunk in sFilters)
                {
                    string[] sFilterParts = sFilterChunk.Split(char.ConvertFromUtf32(201).ToCharArray());
                    string sName = sFilterParts[0];
                    string sValue = sFilterParts[1];

                    switch (sName)
                    {
                        case "Request Number":
                            query.RequestNumber = sValue;
                            break;
                        case "Request Date":
                            DateTime oDateTime = new DateTime();
                            DateTime.TryParse(sValue, out oDateTime);

                            if (oDateTime > DateTime.MinValue)
                            {
                                query.RequestDate = oDateTime;
                            }
                            break;
                        case "Requested By":
                            query.RequestedBy = sValue;
                            break;
                        case "Status":
                            if (!string.IsNullOrEmpty(sValue))
                            {
                                List<int> status = sValue.Split(',').Select(i => int.Parse(i)).ToList();
                                query.SetStatus(status);
                            }

                            break;
                        case "Languages":
                            if (!string.IsNullOrEmpty(sValue))
                            {
                                query.Lanugages.AddRange(sValue.Split(','));
                            }

                            break;
                        case "ASAP":
                            query.RequestASAP = (sValue == "Y") ? true : false;
                            break;
                        case "Labels":
                            if (!string.IsNullOrEmpty(sValue))
                            {
                                List<int> labels = sValue.Split(',').Select(i => int.Parse(i)).ToList();
                                query.SetLabels(labels);
                            }

                            break;
                        case "LabelsShowOnlyUnlabeled":
                            query.LabelsShowOnlyUnlabeled = (sValue == "Y") ? true : false;
                            break;
                        default:
                            break;
                    }
                }
            }

            return query;
        }

        private static void CacheDashboardQuery(Guid userID, StaffRequestDashboardQuery query)
        {
            string cacheKey = string.Format("{0}-{1}", CacheKey, userID);

            if (HttpContext.Current.Cache[cacheKey] == null)
            {
                HttpContext.Current.Cache.Add(cacheKey, query, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), System.Web.Caching.CacheItemPriority.Normal, null);
            }
            else
            {
                HttpContext.Current.Cache[cacheKey] = query;
            }
        }

        /// <summary>
        /// Updates the user's dashboard query perferences in cache and in the DB
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        public static void UpdateStaffRequestDashboardSettings(StaffRequestDashboardQuery query, Guid userID, DataAccessDataContext context)
        {
            CacheDashboardQuery(userID, query);
            UpdateStaffRequestDashboardSettingsInDB(query, userID, context);
        }

        private static void UpdateStaffRequestDashboardSettingsInDB(StaffRequestDashboardQuery query, Guid userID, DataAccessDataContext context)
        {
            IAUserSettingsGrid oIAUserSettingsGrid = context.IAUserSettingsGrids.SingleOrDefault(row => row.MPUserID == userID && row.Name == "Staff Requests");
            if (oIAUserSettingsGrid == null)
            {
                oIAUserSettingsGrid.MPUserID = userID;
                oIAUserSettingsGrid.Name = "Staff Requests";
                oIAUserSettingsGrid.Filters = GetStaffRequestsDashboardFilterSeralized(query);
                oIAUserSettingsGrid.SortExpression = query.OrderBy_ForQuery;
                context.IAUserSettingsGrids.InsertOnSubmit(oIAUserSettingsGrid);
            }
            else
            {
                oIAUserSettingsGrid.Filters = GetStaffRequestsDashboardFilterSeralized(query);
                oIAUserSettingsGrid.SortExpression = query.OrderBy_ForQuery;
            }

            context.SubmitChanges();
        }

        /// <summary>
        /// Searlizes the filter content back to a format that was from telerik earlier
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static string GetStaffRequestsDashboardFilterSeralized(StaffRequestDashboardQuery query)
        {
            string itemSpliter = "È"; //char.ConvertFromUtf32(200)
            string pairSpliter = "É"; //char.ConvertFromUtf32(201)

            List<KeyValuePair<string, string>> filterProperties = new List<KeyValuePair<string, string>>();
            StringBuilder sb = new StringBuilder();

            filterProperties.Add(new KeyValuePair<string, string>("Request Number", query.RequestNumber));
            filterProperties.Add(new KeyValuePair<string, string>("Request Date", query.RequestDate > DateTime.MinValue ? query.RequestDate.ToString() : string.Empty));
            filterProperties.Add(new KeyValuePair<string, string>("Requested By", query.RequestedBy));
            filterProperties.Add(new KeyValuePair<string, string>("Status", query.StatusAsStringList));
            filterProperties.Add(new KeyValuePair<string, string>("Languages", query.LanugagesAsStringList));
            filterProperties.Add(new KeyValuePair<string, string>("ASAP", query.RequestASAP ? "Y" : "N"));
            filterProperties.Add(new KeyValuePair<string, string>("Labels", query.LabelsAsStringList));
            filterProperties.Add(new KeyValuePair<string, string>("LabelsShowOnlyUnlabeled", query.LabelsShowOnlyUnlabeled ? "Y" : "N"));

            foreach (KeyValuePair<string, string> item in filterProperties)
            {
                sb.AppendFormat("{0}{1}{2}{3}", item.Key, pairSpliter, item.Value, itemSpliter);
            }
            string returnValue = sb.ToString();
            returnValue = returnValue.TrimEnd(itemSpliter.ToCharArray());

            return returnValue;
        }
    }
}