SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetRequestLabels](@IARequestID int)
RETURNS TABLE 
AS
RETURN 
(
	select		IALabel.IALabelID,
				IALabel.Text
	from		IARequestLabel
	join		IALabel on
				IALabel.IALabelID = IARequestLabel.IALabelID
	where		IARequestLabel.IARequestID = @IARequestID
)
GO
