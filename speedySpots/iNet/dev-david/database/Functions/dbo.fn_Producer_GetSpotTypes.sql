SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetSpotTypes]()
RETURNS TABLE 
AS
RETURN 
(
	select			IASpotType.IASpotTypeID,
					IASpotType.Name
	from			IASpotType
)
GO
