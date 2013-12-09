SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Admin_GetUserCount](@MPOrgID uniqueidentifier)
RETURNS int
AS
BEGIN
	DECLARE @Result int

	select @Result = (select count(*) from MPOrgUser where MPOrgUser.MPOrgID = @MPOrgID)
	
	RETURN @Result
END
GO
