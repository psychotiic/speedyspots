
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Admin_GetOrganizations]()
RETURNS TABLE 
AS
RETURN 
(
	select			MPOrg.MPOrgID,
					MPOrg.Name,
					dbo.fn_Admin_GetUserCount(MPOrg.MPOrgID) as UserCount,
					MPOrgData.City,
					MPOrgData.State,
					MPOrgData.Zip,
					MPOrgData.Phone,
					MPOrgData.IsVerified
	from			MPOrg
	join			MPOrgData on
					MPOrgData.MPOrgID = MPOrg.MPOrgID
)
GO
