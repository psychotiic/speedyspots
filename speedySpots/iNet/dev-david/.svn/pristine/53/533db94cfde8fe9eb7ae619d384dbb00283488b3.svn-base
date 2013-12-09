SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetAllTalentUnavailability]()
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
)
GO
