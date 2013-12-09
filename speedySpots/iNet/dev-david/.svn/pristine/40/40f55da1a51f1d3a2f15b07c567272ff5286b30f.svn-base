SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetSpotFees](@IASpotID int)
RETURNS TABLE 
AS
RETURN 
(
	select			IASpotFee.IASPotFeeID,
					IASpotFee.Fee,
					IASpotFeeType.Name
	from			IASpotFee
	join			IASpotFeeType on
					IASpotFeeType.IASpotFeeTypeID = IASpotFee.IASpotFeeTypeID
	where			IASpotFee.IASpotID = @IASpotID
)
GO
