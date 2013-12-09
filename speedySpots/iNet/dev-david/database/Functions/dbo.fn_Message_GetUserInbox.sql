SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Message_GetUserInbox](@MPUserID uniqueidentifier)
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
	join		IAMessageRecipient on
				IAMessageRecipient.IAMessageID = IAMessage.IAMessageID
	join		MPUserData on
				MPUserData.MPUserID = IAMessage.MPUserID
	where		IAMessageRecipient.MPUserID = @MPUserID
)
GO
