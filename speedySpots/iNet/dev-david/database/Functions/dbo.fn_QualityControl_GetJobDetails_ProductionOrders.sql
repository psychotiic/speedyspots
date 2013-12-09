SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_QualityControl_GetJobDetails_ProductionOrders](@IAJobID int)
RETURNS TABLE 
AS
RETURN 
(
	select				IAProductionOrder.IAProductionOrderID,
						MPUserData.FirstName + ' ' + MPUserData.LastName as TalentName
	from				IAProductionOrder
	join				MPUser on
						MPUser.MPUserID = IAProductionOrder.MPUserIDTalent
	join				MPUserData on
						MPUserData.MPUserID = MPUser.MPUserID
	where				IAProductionOrder.IAJobID = @IAJobID
)
GO
