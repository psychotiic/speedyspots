SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Message_GetAdminInbox]()
RETURNS TABLE 
AS
RETURN 
(
	select		IAMessage.IAMessageID,
				IAMessage.[Subject],
				MPUserData.FirstName + ' ' + MPUserData.LastName as Sender,
				IAMessage.DisplayStartDateTime,
				IAMessage.DisplayEndDateTime,
				IAMessage.CreatedDateTime
	from		IAMessage
	join		MPUserData on
				MPUserData.MPUserID = IAMessage.MPUserID
)
GO
