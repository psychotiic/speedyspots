SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetLabelFilter]()
RETURNS TABLE 
AS
RETURN 
(
	select			-2 as SortID,
					-1 as IALabelID,
					'All' as Text
	union
	select			-1 as SortID,
					-2 as IALabelID,
					'Unlabeled' as Text
	union
	select			0 as SortID,
					IALabel.IALabelID as IALabelID,
					IALabel.Text as Text
	from			IALabel
)
GO
