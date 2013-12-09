SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_System_GetOrganization](@MPUserID uniqueidentifier)
RETURNS uniqueidentifier
AS
BEGIN
	declare @Result uniqueidentifier
	
	select		@Result = MPOrg.MPOrgID
	from		MPOrg
	join		MPOrgUser on
				MPOrgUser.MPOrgId = MPOrg.MPOrgID and
				MPOrgUser.MPUserId = @MPUserID

	return @Result
END
GO
