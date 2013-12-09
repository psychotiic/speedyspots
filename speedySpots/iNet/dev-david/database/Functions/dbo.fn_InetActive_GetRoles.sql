SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fn_InetActive_GetRoles](@MPOrgID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select		MPRole.MPRoleID,
				MPRole.Name,
				(select count(*) from MPRolePrivilege where MPRolePrivilege.MPRoleID = MPRole.MPRoleID) as PrivilegeCount,
				MPRole.CreatedOn
	from		MPRole
	where		MPRole.MPOrgID = @MPOrgID
)
GO
