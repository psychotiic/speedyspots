
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Customer_GetDashboard](@MPUserID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select			IARequest.IARequestID,
					MPOrg.Name as CompanyName,
					MPUserData.FirstName + ' ' + MPUserData.LastName as UserName,
					IARequest.CreatedDateTime,
					IARequestStatus.Name as Status
	from			IARequest
	join			IARequestStatus on
					IARequestStatus.IARequestStatusID = IARequest.IARequestStatusID
	join			MPUser on
					MPUser.MPUserID = IARequest.MPUserID
	join			MPUserData on
					MPUserData.MPUserID = MPUser.MPUserID
	join			MPOrgUser on
					MPOrgUser.MPUserID = MPUser.MPUserID
	join			MPOrg on
					MPOrg.MPOrgID = MPOrgUser.MPOrgID
	where			IARequest.MPUserID = @MPUserID and
					IARequest.IARequestStatusID not in(select IARequestStatus.IARequestStatusID from IARequestStatus where Name = 'Canceled')
)
GO
