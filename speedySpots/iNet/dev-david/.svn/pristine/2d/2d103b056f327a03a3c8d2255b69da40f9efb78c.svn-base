
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Talent_GetProductionOrders](@MPUserID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select			IAProductionOrder.IAProductionOrderID,
					IAProductionOrder.IAProductionOrderStatusID,
					IAJob.Name,
					IAJob.CompletedDateTime
	from			IAJob
	join			IAProductionOrder on
					IAProductionOrder.IAJobID = IAJob.IAJobID
	where			IAProductionOrder.MPUserIDTalent = @MPUserID
)
GO
