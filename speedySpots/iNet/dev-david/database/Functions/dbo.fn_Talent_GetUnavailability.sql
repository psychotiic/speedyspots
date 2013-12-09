SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Talent_GetUnavailability](@MPUserID uniqueidentifier)
RETURNS TABLE 
AS
RETURN 
(
	select			IATalentUnavailability.IATalentUnavailabilityID,
					IATalentUnavailability.FromDateTime,
					IATalentUnavailability.ToDateTime,
					IATalentUnavailability.Status,
					IATalentUnavailability.CreatedDateTime
	from			IATalentUnavailability
	where			IATalentUnavailability.MPUserID = @MPUserID
)
GO
