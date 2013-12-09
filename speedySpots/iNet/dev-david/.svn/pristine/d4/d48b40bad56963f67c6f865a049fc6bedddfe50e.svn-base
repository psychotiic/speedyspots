SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Report_GetFeedbackType](@FeedbackType nvarchar(50))
RETURNS TABLE 
AS
RETURN 
( 
	select		'Question' as FeedbackType
	union
	select		'Problem' as FeedbackType
	union
	select		'Idea' as FeedbackType
	union
	select		'Praise' as FeedbackType
)
GO
