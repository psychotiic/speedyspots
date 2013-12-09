SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetSpotFeeTypes]()
RETURNS TABLE 
AS
RETURN 
(
	select			IASpotFeeType.IASpotFeeTypeID,
					IASpotFeeType.Name
	from			IASpotFeeType
)
GO
