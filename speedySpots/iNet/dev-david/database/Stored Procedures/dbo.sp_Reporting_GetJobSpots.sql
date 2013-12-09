
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[sp_Reporting_GetJobSpots]
	@IAJobID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select			IAProductionOrder.IAProductionOrderID,
					Talent.FirstName as TalentFirstName,
					Talent.LastName as TalentLastName,
					IASpot.IASpotID,
					IASpot.Title,
					IASpot.LengthActual,
					IASpot.PurchaseOrderNumber,
					IASpotFeeType.Name as SpotFeeTypeName,
					IASpotType.Name as SpotTypeName,
					IASpotFee.Fee as SpotFee
	from			IAJob
	left join		IAProductionOrder on
					IAProductionOrder.IAJobID = IAJob.IAJobID
	left join		MPUserData Talent on
					Talent.MPUserID = IAProductionOrder.MPUserIDTalent
	left join		IARequest on
					IARequest.IARequestID = IAJob.IARequestID
	left join		MPUserData Requester on
					Requester.MPUserID = IARequest.MPUserID
	left join		MPOrg on
					MPOrg.MPOrgID = dbo.fn_System_GetOrganization(IARequest.MPUserID)
	left join		IASpot on
					IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID
	left join		IASpotFee on
					IASpotFee.IASpotID = IASpot.IASpotID
	left join		IASpotFeeType on
					IASpotFeeType.IASpotFeeTypeID = IASpotFee.IASpotFeeTypeID
	left join		IASpotType on
					IASpot.IASpotTypeID	= IASpotType.IASpotTypeID
	where			IAJob.IAJobID = @IAJobID
	order by		IAJob.Name
END
GO
