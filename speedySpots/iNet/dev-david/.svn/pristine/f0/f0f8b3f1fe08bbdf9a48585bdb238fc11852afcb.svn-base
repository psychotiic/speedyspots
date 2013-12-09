SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetSpots](@IAProductionOrderID int)
RETURNS TABLE 
AS
RETURN 
(
	select			IASpot.IASpotID,
					IASpot.Title,
					IASpot.DueDateTime
	from			IASpot
	where			IASpot.IAProductionOrderID = @IAProductionOrderID
)
GO
