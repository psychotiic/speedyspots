SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_QualityControl_GetDashboardSpots](@IAJobID int)
RETURNS TABLE 
AS
RETURN 
(
	select			IAJob.IAJobID,
					IAProductionOrder.IAProductionOrderID,
					IASpot.IASpotID,
					IASpot.IsAsap,
					IASpot.IASpotStatusID,
					IASpot.Title,
					(select FirstName + ' ' + LastName from MPUserData where MPUserID = IAProductionOrder.MPUserIDTalent) as Talent,
					IASpot.DueDateTime,
					IASpotStatus.Name as SpotStatus
	from			IAJob
	join			IAProductionOrder on
					IAProductionOrder.IAJobID = IAJob.IAJobID
	join			IASpot on
					IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID
	join			IASpotStatus on
					IASpotStatus.IASpotStatusID = IASpot.IASpotStatusID
	where			IAJob.IAJobID = @IAJobID
)
GO
