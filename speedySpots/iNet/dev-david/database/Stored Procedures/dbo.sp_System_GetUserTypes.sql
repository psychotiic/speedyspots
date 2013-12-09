
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[sp_System_GetUserTypes]
AS
BEGIN
	SET NOCOUNT ON;

    select		'Customer' as UserType
	union
	select		'Staff' as UserType
	union
	select		'Talent' as UserType
	union
	select		'Unknown' as UserType
END
GO
