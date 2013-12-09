
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Site_GetRequestStatuses]()
RETURNS TABLE 
AS
RETURN 
(
	select			IARequestStatus.IARequestStatusID,
					IARequestStatus.Name,
					IARequeststatus.SortOrder
	from			IARequestStatus
)
GO
