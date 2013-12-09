SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fn_Admin_GetAllUsers]()
RETURNS TABLE 
AS
RETURN 
(
	select		MPUser.MPUserID,
				MPUser.Username,
				MPUserData.FirstName + ' ' + MPUserData.LastName as Name
	from		MPUser
	join		MPUserData on
				MPUserData.MPUserID = MPUser.MPUserID
	join		MPOrgUser on
				MPOrgUser.MPUserID = MPUser.MPUserID
)
GO
