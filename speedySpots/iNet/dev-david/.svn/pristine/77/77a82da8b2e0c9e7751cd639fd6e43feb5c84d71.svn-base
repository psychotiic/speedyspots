using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public enum StatusType
    {
        JobStatus,
        ProductionOrderStatus,
        RequestStatus,
        SpotStatus
    }

    public static class StatusService
    {

        const string CacheItemKey = "System-TypeStatuses";

        public static Dictionary<StatusType, List<StatusOption>> GetStatuses(DataAccessDataContext context)
        {
            Dictionary<StatusType, List<StatusOption>> statues = new Dictionary<StatusType, List<Models.StatusOption>>();

            if (HttpContext.Current.Cache[CacheItemKey] == null)
            {
                statues.Add(StatusType.JobStatus, GetJobStatusesFromDB(context));
                statues.Add(StatusType.ProductionOrderStatus, GetProductionOrderStatusesFromDB(context));
                statues.Add(StatusType.RequestStatus, GetRequestStatusesFromDB(context));
                statues.Add(StatusType.SpotStatus, GetSpotStatusesFromDB(context));
                HttpContext.Current.Cache.Add(CacheItemKey, statues, null, DateTime.Now.AddSeconds(14400), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Normal, null);
            }
            else
            {
                statues = (Dictionary<StatusType, List<StatusOption>>)HttpContext.Current.Cache[CacheItemKey];
            }

            return statues;
        }


        private static List<StatusOption> GetJobStatusesFromDB(DataAccessDataContext context)
        {
            List<StatusOption> jobStatuses = (from n in context.IAJobStatus
                                               select new StatusOption
                                               {
                                                   ID = n.IAJobStatusID,
                                                   Name = n.Name
                                               }).ToList();
            return jobStatuses;
        }

        private static List<StatusOption> GetProductionOrderStatusesFromDB(DataAccessDataContext context)
        {
            List<StatusOption> productionOrderStatuses = (from n in context.IAProductionOrderStatus
                                               select new StatusOption
                                               {
                                                   ID = n.IAProductionOrderStatusID,
                                                   Name = n.Name
                                               }).ToList();
            return productionOrderStatuses;
        }

        private static List<StatusOption> GetRequestStatusesFromDB(DataAccessDataContext context)
        {
            List<StatusOption> requestStatuses = (from n in context.IARequestStatus
                                                  orderby n.SortOrder
                                                          select new StatusOption
                                                          {
                                                              ID = n.IARequestStatusID,
                                                              Name = n.Name
                                                          }).ToList();
            return requestStatuses;
        }

        private static List<StatusOption> GetSpotStatusesFromDB(DataAccessDataContext context)
        {
            List<StatusOption> spotStatuses = (from n in context.IASpotStatus
                                               select new StatusOption
                                               {
                                                   ID = n.IASpotStatusID,
                                                   Name = n.Name
                                               }).ToList();
            return spotStatuses;
        }


        
    }
}