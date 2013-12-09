SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Producer_GetJobFiles](@IAJobID int)
RETURNS TABLE 
AS
RETURN 
(
	select			IAJobFile.IAJobFileID,
					IAJobFile.Filename,
					IAJobFileType.Name as FileTypeName
	from			IAJobFile
	join			IAJobFileType on
					IAJobFileType.IAJobFileTypeID = IAJobFile.IAJobFileTypeID
	where			IAJobFile.IAJobID = @IAJobID
)
GO
