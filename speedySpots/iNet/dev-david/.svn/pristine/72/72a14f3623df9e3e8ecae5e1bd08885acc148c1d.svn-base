
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Admin_GetPossibleDuplicateOrganizations](@MPOrgID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select			MPOrg.MPOrgID,
					MPOrg.Name
	from			MPOrg
	join			MPOrgData on
					MPOrgData.MPOrgID = MPOrg.MPOrgID
	where			(
						(MPOrg.Name like (select '%' + MPOrg.Name + '%' from MPOrg where MPOrg.MPOrgID = @MPOrgID) and MPOrgData.Zip like (select '%' + MPOrgData.Zip + '%' from MPOrgData where MPOrgData.MPOrgID = @MPOrgID)) or
						MPOrgData.Address1 like (select '%' + MPOrgData.Address1 + '%' from MPOrgData where MPOrgData.MPOrgID = @MPOrgID) or
						MPOrgData.Phone like (select '%' + MPOrgData.Phone + '%' from MPOrgData where MPOrgData.MPOrgID = @MPOrgID)
					) and
					MPOrg.MPOrgID != @MPOrgID and
					MPOrgData.IsVerified = 'Y'
)
GO
