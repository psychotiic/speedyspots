SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Message_GetRecipients](@IAMessageID int)
RETURNS TABLE 
AS
RETURN 
(
	select		IAMessage.IAMessageID,
				MPUserData.FirstName + ' ' + MPUserData.LastName as Recipient,
				IAMessageRecipient.IsAcknowledged,
				IAMessageRecipient.AcknowledgedDateTime
	from		IAMessage
	join		IAMessageRecipient on
				IAMessageRecipient.IAMessageID = IAMessage.IAMessageID
	join		MPUserData on
				MPUserData.MPUserID = IAMessageRecipient.MPUserID
	where		IAMessage.IAMessageID = @IAMessageID
)
GO
