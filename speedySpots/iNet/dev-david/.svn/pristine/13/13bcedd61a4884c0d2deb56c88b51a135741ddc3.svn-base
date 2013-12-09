
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Admin_GetDashboard]()
RETURNS TABLE 
AS
RETURN 
(
	select			MPUser.MPUserID,
					MPUser.Username,
					MPUserData.LastName + ', ' + MPUserData.FirstName as Name,
					MPUserData.IsCustomer,
					MPUserData.IsStaff,
					MPUserData.IsTalent,
					MPUserData.IsAdmin,
					MPUser.LastLoginOn
	from			MPUser
	join			MPUserData on
					MPUserData.MPUserID = MPUser.MPUserID
)
GO
