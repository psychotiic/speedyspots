SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Talent_GetDashboard](@MPUserID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select			IAJob.IAJobID,
					IAProductionOrder.IAProductionOrderID,
					IAProductionOrder.IAProductionOrderStatusID,
					IAJob.IsAsap,
					IAProductionOrder.HasBeenViewedByTalent,
					IAJob.Name as JobName,
					(select count(*) from IASpot where IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID and IASpot.IASpotStatusID not in (select IASpotStatusID from IASpotStatus where Name = 'On Hold')) as SpotsNotOnHold,
					(select count(*) from IASpot where IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID) as SpotCount,
					(select top 1 IASpot.DueDateTime from IASpot where IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID and IASpot.IASpotStatusID not in (select IASpotStatusID from IASpotStatus where Name = 'On Hold') order by DueDateTime desc) as DueDateTime,
					IAJobStatus.Name as Status
	from			IAProductionOrder
	join			IAJob on
					IAJob.IAJobID = IAProductionOrder.IAJobID
	join			IAJobStatus on
					IAJobStatus.IAJobStatusID = IAJob.IAJobStatusID
	where			IAProductionOrder.MPUserIDTalent = @MPUserID
)

GO
