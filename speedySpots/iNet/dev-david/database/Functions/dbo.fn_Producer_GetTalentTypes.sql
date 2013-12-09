SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetTalentTypes]()
RETURNS TABLE 
AS
RETURN 
(
	select			IATalentType.IATalentTypeID,
					IATalentType.Name
	from			IATalentType
)
GO
