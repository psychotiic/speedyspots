SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetProductionOrders](@IAJobID int)
RETURNS TABLE 
AS
RETURN 
(
	select			IAJob.IAJobID,
					IAProductionOrder.IAProductionOrderID,
					TalentUserData.FirstName + ' ' + TalentUserData.LastName as Talent,
					(select count(*) from IASpot where IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID) as spots,
					IAProductionOrderStatus.Name as Status
	from			IAProductionOrder
	join			IAJob on
					IAJob.IAJobID = IAProductionOrder.IAJobID
	join			MPUser TalentUser on
					TalentUser.MPUserID = IAProductionOrder.MPUserIDTalent
	join			MPUserData TalentUserData on
					TalentUserData.MPUserID = TalentUser.MPUserID
	join			IAProductionOrderStatus on
					IAProductionOrderStatus.IAProductionOrderStatusID = IAProductionOrder.IAProductionOrderStatusID
	where			IAProductionOrder.IAJobID = @IAJobID				
)
GO
