SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE PROCEDURE [dbo].[sp_Reporting_GetRequestNotes]
	@IARequestID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	select			IARequestNote.CreatedDateTime,
					IARequestNote.Note,
					MPUserData.FirstName,
					MPUserData.LastName
	from			IARequestNote
	join			MPUserData on
					MPUserData.MPUserID = IARequestNote.MPUserID
	where			IARequestNote.IARequestID = @IARequestID
END
GO
