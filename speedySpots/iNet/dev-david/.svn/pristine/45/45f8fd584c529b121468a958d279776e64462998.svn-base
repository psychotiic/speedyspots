
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Customer_GetUserAccountCompanies]()
RETURNS TABLE 
AS
RETURN 
(
	select			MPOrg.MPOrgID,
					MPOrg.Name + ', ' + MPorgData.City as Name,
					MPOrgData.IsVerified
	from			MPOrg
	join			MPOrgData on
					MPOrgData.MPOrgID = MPOrg.MPOrgID
)
GO
