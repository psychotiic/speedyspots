
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_QualityControl_GetDashboardJobs](@MPUserIDTalent uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select			IAJob.IAJobID,
					IAJob.IARequestID,
					IAJob.Sequence,
					dbo.fn_System_DisplayJobID(IAJob.IARequestID, IAJob.Sequence) as DisplayJobID,
					IAJob.IsAsap,
					IAJob.Language,
					IAJobStatus.IAJobStatusID,
					(select count(*) from IASpot where IASpot.IAProductionOrderID in(select IAProductionOrder.IAProductionOrderID from IAProductionOrder where IAProductionOrder.IAJobID = IAJob.IAJobID) and IASpot.IASpotStatusID not in (select IASpotStatusID from IASpotStatus where Name = 'On Hold')) as SpotsNotOnHold,
					IAJob.Name as JobName,
					IAJob.DueDateTime,
					IAJobStatus.Name as JobStatus,
					(
						select		COUNT(*)
						from		IASpot
						join		IASpotFile on
									IASpotFile.IASpotID = IASpot.IASpotID
						where		IASpot.IAProductionOrderID in (select IAProductionOrder.IAProductionOrderID from IAProductionOrder where IAProductionOrder.IAJobID = IAJob.IAJobID) and
									IASpotFile.IASpotFileTypeID = (select IASpotFileType.IASpotFileTypeID from IASpotFileType where IASpotFileType.Name = 'Talent')
					) as TalentFileCount,
					case when DateDiff(minute, IAJob.DueDateTime, GETDATE()) > 0 then 1 else 0 end as IsPastDue
	from			IAJob
	join			IAJobStatus on
					IAJobStatus.IAJobStatusID = IAJob.IAJobStatusID
	where			@MPUserIDTalent in(select IAProductionOrder.MPUserIDTalent from IAProductionOrder where IAProductionOrder.IAJobID = IAJob.IAJobID union select '00000000-0000-0000-0000-000000000000')
)
GO
