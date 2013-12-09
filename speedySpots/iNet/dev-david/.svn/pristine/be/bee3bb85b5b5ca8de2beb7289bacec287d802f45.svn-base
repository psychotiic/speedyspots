SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetAllTalent]()
RETURNS TABLE 
AS
RETURN 
(
	select			MPUser.MPUserID,
					MPUserData.FirstName + ' ' + MPUserData.LastName as UserName
	from			MPUser
	join			MPUserData on
					MPUserData.MPUserID = MPUser.MPUserID
	where			MPUserData.IsTalent = 'Y'
)
GO
