SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetUnavailabilityReport](@StartOfToday datetime, @EndOfToday datetime)
RETURNS TABLE 
AS
RETURN 
(
	select			IATalentUnavailability.IATalentUnavailabilityID,
					IATalentUnavailability.MPUserID,
					MPUserData.FirstName + ' ' + MPUserData.LastName as Talent,
					IATalentUnavailability.FromDateTime,
					IATalentUnavailability.ToDateTime,
					IATalentUnavailability.Status,
					IATalentUnavailability.Notes,
					IATalentUnavailability.CreatedDateTime
	from			IATalentUnavailability
	join			MPUserData on
					MPUserData.MPUserID = IATalentUnavailability.MPUserID
	where			Status = 'Approved' and
					(
						(IATalentUnavailability.FromDateTime between '1900-1-1 00:00:00.000' and @EndOfToday)
						and
						(IATalentUnavailability.ToDateTime between @StartOfToday and '2100-12-31 23:59:59.999')
					)
)
GO
