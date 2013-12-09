using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class JobsService
    {
        public static StatusOption GetStatusOption(JobStatus status, DataAccessDataContext context)
        {
            return StatusService.GetStatuses(context)[StatusType.JobStatus].Where(s => s.SystemName == status.ToString()).SingleOrDefault();
        }

        public static bool AreAllProducitonOrdersComplete(IAJob job, DataAccessDataContext context)
        {
            StatusOption completeStatus = ProductionOrdersService.GetStatusOption(ProductionOrderStatus.Complete, context);

            int completedProductionOrders = job.IAProductionOrders.Where(row => row.IAProductionOrderStatusID == completeStatus.ID).Count();
            int totalProductionOrders = job.IAProductionOrders.Count();

            if (totalProductionOrders == completedProductionOrders)
            {
                return true;
            }

            return false;
        }

        public static int GetNextJobSequenceNumberForRequest(int requestID, DataAccessDataContext context)
        {
            int sequence = 1;
            IAJob job = context.IAJobs.Where(row => row.IARequestID == requestID).OrderByDescending(row => row.Sequence).Take(1).SingleOrDefault();
            if (job != null)
            {
                sequence = job.Sequence + 1;
            }

            return sequence;
        }

    }
}