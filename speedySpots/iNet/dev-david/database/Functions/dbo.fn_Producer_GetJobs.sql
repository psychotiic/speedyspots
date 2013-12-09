SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetJobs](@IARequestID int)
RETURNS TABLE 
AS
RETURN 
(
	select				IAJob.IAJobID,
						IAJob.IARequestID,
						IAJob.Sequence,
						IAJob.Name,
						IAJob.DueDateTime
	from				IAJob
	where				IAJob.IARequestID = @IARequestID
)
GO
