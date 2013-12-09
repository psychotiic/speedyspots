SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fn_InetActive_GetPrivileges]()
RETURNS TABLE 
AS
RETURN 
(
	select		MPPrivilege.MPPrivilegeCategoryID as ParentID,
				MPPrivilege.MPPrivilegeID,
				MPPrivilege.Name,
				MPPrivilege.Code
	from		MPPrivilege
	union		all
	select		null as ParentID,
				MPPrivilegeCategory.MPPrivilegeCategoryID,
				MPPrivilegeCategory.Name,
				'' as Code
	from		MPPrivilegeCategory
)
GO
