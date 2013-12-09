SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[sp_System_GetUserRoles]
	@MPUserID uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

    select		'Customer' as UserType
    from		MPUserData
    where		MPUserData.MPUserID = @MPUserID and
				MPUserData.IsCustomer = 'Y'
	union
	select		'Staff' as UserType
    from		MPUserData
    where		MPUserData.MPUserID = @MPUserID and
				MPUserData.IsStaff = 'Y'
	union
	select		'Talent' as UserType
    from		MPUserData
    where		MPUserData.MPUserID = @MPUserID and
				MPUserData.IsTalent = 'Y'
END
GO
