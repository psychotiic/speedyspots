
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[sp_Reporting_GetJobs]
	@StartDateTime datetime,
	@EndDateTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select			MPOrg.Name as CompanyName,
					IARequest.IARequestID,
					IAJob.IAJobID,
					IAJob.Sequence,
					IARequest.CreatedDateTime,
					IAJob.CreatedDateTime as JobCreatedDateTime,
					IAJob.CompletedDateTime as JobCompletedDateTime,
					Requester.FirstName as RequesterFirstName,
					Requester.LastName as RequesterLastName,
					Creator.FirstName as CreatorFirstName,
					Creator.LastName as CreatorLastName,
					Completor.FirstName as CompletorFirstName,
					Completor.LastName as CompletorLastName,
					IAJob.Name,
					IAJob.QuantityMusic,
					IAJob.QuantitySFX,
					IAJob.QuantityProduction,
					IAJob.QuantityConvert,
					IAJob.PriceMusic,
					IAJob.PriceSFX,
					IAJob.PriceProduction,
					IAJob.PriceConvert,
					(
							select			COUNT(*)
							from			IASpot
							join			IAProductionOrder on
											IAProductionOrder.IAJobID = IAJob.IAJobID
							where			IAProductionOrder.IAProductionOrderID = IAspot.IAProductionOrderID
					) as TotalReads,
					isnull(
						(
							select			sum(IASpotFee.Fee)
							from			IASpot
							left join		IAProductionOrder on
											IAProductionOrder.IAJobID = IAJob.IAJobID
							left join		IASpotFee on
											IASpotFee.IASpotID = IASpot.IASpotID
							where			IASpot.IAProductionOrderID = IAProductionOrder.IAProductionOrderID
						) +
						(IAJob.QuantityMusic * IAJob.PriceMusic) + (IAJob.QuantitySFX * IAJob.PriceSFX) + (IAJob.QuantityConvert * IAJob.PriceConvert) + (IAJob.QuantityProduction * IAJob.PriceProduction)
					,0) as GrandTotal
	from			IAJob
	left join		IARequest on
					IARequest.IARequestID = IAJob.IARequestID
	left join		MPUserData Creator on
					Creator.MPUserID = IAJob.MPUserID
	left join		MPUserData Completor on
					Completor.MPUserID = IAJob.MPUserIDCompleted
	left join		MPUserData Requester on
					Requester.MPUserID = IARequest.MPUserID
	left join		MPOrg on
					MPOrg.MPOrgID = dbo.fn_System_GetOrganization(IARequest.MPUserID)
	where			IAJob.CompletedDateTime between @StartDateTime and dateadd(day, 1, @EndDatetime)
	order by		IAJob.Name
END
GO
