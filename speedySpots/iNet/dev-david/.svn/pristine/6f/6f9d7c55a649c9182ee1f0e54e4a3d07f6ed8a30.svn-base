SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetCustomerLookup]()
RETURNS TABLE 
AS
RETURN 
(
	select		MPUser.MPUserID,
				MPUser.Username,
				MPUserData.FirstName,
				MPUserData.LastName,
				isnull((
					select		top 1
								MPOrg.Name
					from		MPOrg
					join		MPOrgUser on
								MPOrgUser.MPOrgID = MPOrg.MPOrgID				
					where		MPOrgUser.MPUserID = MPUser.MPUserID
				), 'N/A') as CompanyName
	from		MPUser
	join		MPUserData on
				MPUserData.MPUserID = MPUser.MPUserID
	where		MPUserData.IsCustomer = 'Y'
)
GO
