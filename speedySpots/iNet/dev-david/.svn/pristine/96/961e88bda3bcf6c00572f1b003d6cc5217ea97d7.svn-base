SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE FUNCTION [dbo].[fn_InetActive_GetUsers]()
RETURNS TABLE 
AS
RETURN 
(
	select		MPUser.MPUserID,
				MPUser.Username,
				MPUser.IsLocked,
				MPUserData.FirstName,
				MPUserData.LastName
	from		MPUser
	join		MPUserData on
				MPUserData.MPUserID = MPUser.MPUserID
)
GO
