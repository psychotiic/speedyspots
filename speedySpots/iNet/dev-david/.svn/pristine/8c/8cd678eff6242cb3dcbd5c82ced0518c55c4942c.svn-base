using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeedySpots.DataAccess;
using SpeedySpots.Business.Models;

namespace SpeedySpots.Business.Services
{
    public static class SpotService
    {
        public static void MarkSpotForReRecord(IASpot spot, DataAccessDataContext context)
        {
            spot.IAProductionOrder.IAJob.IAJobStatusID = JobsService.GetStatusOption(JobStatus.Incomplete, context).ID;
            spot.IAProductionOrder.IAProductionOrderStatusID = ProductionOrdersService.GetStatusOption(ProductionOrderStatus.Incomplete, context).ID;
            spot.IASpotStatusID = GetStatusOption(SpotStatus.NeedsFix, context).ID;

            // For each file in the spot that is a talent file, mark them as not deletable
            SpotFileType spotFileType = SpotFileTypeService.GetSpotTypeByName("Talent", context);
            foreach (IASpotFile oIASpotFile in spot.IASpotFiles.Where(row => row.IASpotFileTypeID == spotFileType.SpotFileTypeId))
            {
                oIASpotFile.IsDeletable = false;
            }

            context.SubmitChanges();
        }

        /// <summary>
        /// Attemps to place the spot, it's associated Production Order and Job back into their previous state based off of each of their completed dates
        /// </summary>
        /// <param name="spot"></param>
        /// <param name="context"></param>
        public static void CancelSpotReRecordRequest(IASpot spot, DataAccessDataContext context)
        {
            spot = SetSpotToPreviousStatus(spot, context);

            bool allSpotsFinishedWithinPO = ProductionOrdersService.AreAllSpotsFinished(spot.IAProductionOrder, context);
            bool allProductionOrdersComplete = JobsService.AreAllProducitonOrdersComplete(spot.IAProductionOrder.IAJob, context);
            bool jobWasPreviouslyCompleted = spot.IAProductionOrder.IAJob.WasJobPreviouslyCompleted();
            JobStatus jobStatus = JobStatus.Incomplete;
            ProductionOrderStatus productionOrderStatus = ProductionOrderStatus.Incomplete;

            if (allSpotsFinishedWithinPO && allProductionOrdersComplete)
            {
                //Production orders and spots are completed, if the job was previously completed, then fast forward it back to that
                jobStatus = jobWasPreviouslyCompleted ? JobStatus.Complete : JobStatus.CompleteNeedsProduction;
                productionOrderStatus = ProductionOrderStatus.Complete;
            }
            else if (allSpotsFinishedWithinPO && !allProductionOrdersComplete)
            {
                //At least one PO isn't complete within the parent job, but all the spots within this PO are completed
                jobStatus = JobStatus.Incomplete;
                productionOrderStatus = ProductionOrderStatus.Complete;
            }
            else if (!allSpotsFinishedWithinPO && !allProductionOrdersComplete)
            {
                //This PO and maybe others are still not completed
                jobStatus = JobStatus.Incomplete;
                productionOrderStatus = ProductionOrderStatus.Incomplete;
            }

            spot.IAProductionOrder.IAJob.IAJobStatusID = JobsService.GetStatusOption(jobStatus, context).ID;
            spot.IAProductionOrder.IAProductionOrderStatusID = ProductionOrdersService.GetStatusOption(productionOrderStatus, context).ID;
            context.SubmitChanges();
        }

        private static IASpot SetSpotToPreviousStatus(IASpot spot, DataAccessDataContext context)
        {
            SpotStatus spotStatus = SpotService.GetSpotPreviousStatus(spot);
            spot.IASpotStatusID = GetStatusOption(spotStatus, context).ID;
            context.SubmitChanges();

            if (spot.IAProductionOrder.IAProductionOrderStatusID == ProductionOrdersService.GetStatusOption(ProductionOrderStatus.Incomplete, context).ID)
            {
                if (ProductionOrdersService.AreAllSpotsFinished(spot.IAProductionOrder, context))
                {
                    spot.IAProductionOrder.IAProductionOrderStatusID = ProductionOrdersService.GetStatusOption(ProductionOrderStatus.Complete, context).ID;
                    context.SubmitChanges();
                }
            }

            return spot;
        }

        /// <summary>
        /// Determins what a spots previous status was based on dates and flags in the spot and it's PO
        /// </summary>
        /// <param name="spot"></param>
        /// <returns></returns>
        public static SpotStatus GetSpotPreviousStatus(IASpot spot)
        {
            if (spot.WasSpotPreviouslyCompleted())
            {
                return SpotStatus.Finished;
            }
            else
            {
                if (spot.IAProductionOrder.IAJob.ProductionDateTime >= spot.IAProductionOrder.OnHoldDateTime)
                {
                    return (spot.IAProductionOrder.HasBeenViewedByTalent) ? SpotStatus.Viewed : SpotStatus.Unviewed;
                }
                else
                {
                    return SpotStatus.OnHold;
                }
            }
        }

        public static StatusOption GetStatusOption(SpotStatus status, DataAccessDataContext context)
        {
            return StatusService.GetStatuses(context)[StatusType.SpotStatus].Where(s => s.SystemName == status.ToString()).SingleOrDefault();
        }

    }
}
