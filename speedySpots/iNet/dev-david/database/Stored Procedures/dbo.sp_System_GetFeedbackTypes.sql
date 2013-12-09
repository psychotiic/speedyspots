SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[sp_System_GetFeedbackTypes]
AS
BEGIN
	SET NOCOUNT ON;

    select		'Problem' as FeedbackType
	union
	select		'Question' as FeedbackType
	union
	select		'Idea' as FeedbackType
	union
	select		'Praise' as FeedbackType
END
GO
