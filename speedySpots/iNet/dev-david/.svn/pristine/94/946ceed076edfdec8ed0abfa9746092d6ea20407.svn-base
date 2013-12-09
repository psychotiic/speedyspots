SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_System_DisplayJobID](@IARequestID int, @Sequence int)
RETURNS nvarchar(15)
AS
BEGIN
	declare @Result nvarchar(15)
	
	select	@Result = REPLICATE('0', 10 - (CONVERT(Int,DATALENGTH(CONVERT(NVarChar, @IARequestID)) / 2))) + (CONVERT(NVarChar, @IARequestID)) + '-' + (CONVERT(NVarChar, @Sequence))

	return @Result
END
GO
