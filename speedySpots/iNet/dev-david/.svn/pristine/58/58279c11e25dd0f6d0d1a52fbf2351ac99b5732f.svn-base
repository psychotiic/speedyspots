SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetSpotFiles](@IASpotID int, @IASpotFileTypeID int)
RETURNS TABLE 
AS
RETURN 
(
	select			IASpotFile.IASpotFileID,
					IASpotFile.Filename
	from			IASpotFile
	where			IASpotFile.IASpotID = @IASpotID and
					IASpotFile.IASpotFileTypeID = @IASpotFileTypeID
)
GO
