SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fn_InetActive_GetOrganizations]()
RETURNS TABLE 
AS
RETURN 
(
	select		MPOrg.MPOrgID,
				MPOrg.Name
	from		MPOrg
)
GO
