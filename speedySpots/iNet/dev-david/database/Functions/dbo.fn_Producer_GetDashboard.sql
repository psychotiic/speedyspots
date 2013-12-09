
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetDashboard](@Labels nvarchar(1000))
RETURNS TABLE 
AS
RETURN 
(
	select			IARequest.IARequestID,
					MPOrg.Name as CompanyName,
					MPUserData.FirstName + ' ' + MPUserData.LastName as UserName,
					IARequest.CreatedDateTime,
					IARequest.IsRushOrder,
					IARequestStatus.Name as Status,
					case when
						(select count(*) from IAJob where IAJob.IARequestID = IARequest.IARequestID and datediff(minute, DueDateTime, GETDATE()) > 0) > 0 then 1 else 0
					end as IsPastDue,
					-- Additional Filters
					IARequest.IARequestStatusID,
					MPUser.Username as Email,
					(select count(*) from IARequestLabel where IARequestLabel.IARequestID = IARequest.IARequestID) as LabelCount,
					(select count(*) from IAJob where IAJob.IARequestID = IARequest.IARequestID) as JobCount,
					(select count(*) from IAJob where IAJob.IARequestID = IARequest.IARequestID and Language = 'English') as EnglishCount,
					(select count(*) from IAJob where IAJob.IARequestID = IARequest.IARequestID and Language = 'Spanish') as SpanishCount
	from			IARequest
	join			IARequestStatus on
					IARequestStatus.IARequestStatusID = IARequest.IARequestStatusID
	join			MPUser on
					MPUser.MPUserID = IARequest.MPUserID
	join			MPUserData on
					MPUserData.MPUserID = MPUser.MPUserID
	join			MPOrgUser on
					MPOrgUser.MPUserID = MPUser.MPUserID
	join			MPOrg on
					MPOrg.MPOrgID = MPOrgUser.MPOrgID
	where			IARequest.IARequestID in
					(
						select
						case when len(@Labels) > 0 then
							(
								select			top 1
												IARequestLabel.IARequestID
								from			IARequestLabel
								where			IARequestLabel.IARequestID = IARequest.IARequestID
								and				IARequestLabel.IALabelID in(select value from dbo.fn_System_Split(@Labels, ','))
							)
						else
							(
								select			DupeRequest.IARequestID
								from			IARequest DupeRequest
								where			DupeRequest.IARequestID = IARequest.IARequestID
							)
						end
					)
)
GO
