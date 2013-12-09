SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Customer_GetRequestLabels](@IARequestID int)
RETURNS TABLE 
AS
RETURN 
(
	select		IALabel.IALabelID,
				IALabel.Text
	from		IARequestLabel
	join		IALabel on
				IALabel.IALabelID = IARequestLabel.IALabelID and
				IALabel.IsCustomerVisible = 1
	where		IARequestLabel.IARequestID = @IARequestID
)
GO
