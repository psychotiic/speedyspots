SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_QualityControl_GetJobTalentFiles](@IAJobID int) 
RETURNS TABLE 
AS
RETURN 
(
	select			IASpotFile.IASpotFileID,
					IASpotFile.Filename
	from			IAJob
	join			IAProductionOrder on
					IAProductionOrder.IAJobID = IAJob.IAJobID
	join			IASpot on
					IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID
	join			IASpotFile on
					IASpotFile.IASpotID = IASpot.IASpotID and
					IASpotFile.IASpotFileTypeID = (select IASpotFileTypeID from IASpotFileType where Name = 'Talent')
	where			IAJob.IAJobID = @IAJobID
)
GO
