
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[sp_Reporting_GetNewCompanyRegistrations]
	@StartDateTime datetime,
	@EndDateTime datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select			MPOrg.MPOrgID,
					MPOrg.Name as CompanyName,
					(
						select			MPUserData.FirstName + ' ' + MPUserData.LastName
						from			MPUserData
						where			MPUserData.MPUserID = (select top 1 isnull(MPOrgUser.MPUserID, '00000000-0000-0000-0000-000000000000') from MPOrgUser where MPOrgUser.MPOrgID = MPOrg.MPOrgID)
					) as UserName,
					MPOrgData.Address1,
					MPOrgData.Address2,
					MPOrgData.City,
					MPOrgData.State,
					MPOrgData.Zip,
					MPOrgData.Phone,
					MPOrgData.Fax,
					MPOrgData.BillingAddress1,
					MPOrgData.BillingAddress2,
					MPOrgData.BillingCity,
					MPOrgData.BillingState,
					MPOrgData.BillingZip,
					MPOrgData.BillingName,
					MPOrgData.BillingPhone,
					MPOrgData.EmailInvoice,
					MPOrg.CreatedOn
	from			MPOrg
	join			MPOrgData on MPOrgData.MPOrgID = MPOrg.MPOrgID
	where			MPOrg.CreatedOn between @StartDateTime and dateadd(day, 1, @EndDatetime) and
					MPOrgData.IsVerified <> 'Y'
	order by		MPOrg.Name
END
GO
