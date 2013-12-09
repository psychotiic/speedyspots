
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[sp_Reporting_GetFeedback]
	@StartDateTime datetime,
	@EndDateTime datetime,
	@UserType nvarchar(50),
	@FeedbackType nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

    select			isnull(MPUser.Username, 'N/A') as Username,
					isnull(MPUserData.FirstName, 'N/A') + ' ' + isnull(MPUserData.LastName, 'N/A') as Name,
					isnull(MPUserData.IsCustomer, 0) as IsCustomer,
					isnull(MPUserData.IsStaff, 0) as IsStaff,
					isnull(MPUserData.IsTalent, 0) as IsTalent,
					IAFeedback.Type,
					IAFeedback.Feeling,
					IAFeedback.Message,
					IAFeedback.CreatedDateTime,
					IAFeedback.Filename
    from			IAFeedback
    left join		MPUser on
					MPUser.MPUserID = IAFeedback.MPUserID
	left join		MPUserData on
					MPUserData.MPUserID = MPUser.MPUserID
	where			IAFeedback.CreatedDateTime between @StartDateTime and dateadd(day, 1, @EndDatetime) and
					IAFeedback.Type in(select value from dbo.fn_System_Split(@FeedbackType, ',')) and				
					isnull((
						select		case
										when MPUserData.IsCustomer = 'Y' then 'Customer'
										when MPUserData.IsStaff = 'Y' then 'Staff'
										when MPUserData.IsTalent = 'Y' then 'Talent'
									end as UserType
						from		MPUserData
						where		MPUserData.MPUserID = MPUser.MPUserID
					), 'Unknown')
					in(select value from dbo.fn_System_Split(@UserType, ','))					
	order by		CreatedDateTime
END
GO
