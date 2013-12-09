SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Message_GetNew](@MPUserID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select		IAMessage.IAMessageID,
				IAMessage.[Subject],
				IAMessage.DisplayStartDateTime,
				IAMessage.DisplayEndDateTime,
				IAMessage.CreatedDateTime
	from		IAMessage
	join		IAMessageRecipient on
				IAMessageRecipient.IAMessageID = IAMessage.IAMessageID
	where		IAMessageRecipient.MPUserID = @MPUserID and
				IAMessageRecipient.IsAcknowledged = 0
)
GO
