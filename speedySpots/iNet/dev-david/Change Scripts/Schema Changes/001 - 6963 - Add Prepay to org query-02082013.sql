/****** Object:  UserDefinedFunction [dbo].[fn_Admin_GetOrganizations]    Script Date: 02/08/2013 07:33:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[fn_Admin_GetOrganizations]()
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
					MPOrgData.IsVerified,
					MPOrgData.IsArchived,
					MPOrgData.IsPayFirst
	from			MPOrg
	join			MPOrgData on
					MPOrgData.MPOrgID = MPOrg.MPOrgID
)
