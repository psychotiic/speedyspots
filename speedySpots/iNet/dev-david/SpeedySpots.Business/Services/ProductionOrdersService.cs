using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class ProductionOrdersService
    {
        public static StatusOption GetStatusOption(ProductionOrderStatus status, DataAccessDataContext context)
        {
            return StatusService.GetStatuses(context)[StatusType.ProductionOrderStatus].Where(s => s.SystemName == status.ToString()).SingleOrDefault();
        }

        public static bool AreAllSpotsFinished(IAProductionOrder po, DataAccessDataContext context)
        {
            StatusOption completeStatus = SpotService.GetStatusOption(SpotStatus.Finished, context);
            int totalSpots = po.IASpots.Count();
            int spotsFinished = po.IASpots.Where(row => row.IASpotStatusID == completeStatus.ID).Count();

            if (totalSpots == spotsFinished)
            {
                return true;
            }

            return false;
        }

    }
}