SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Admin_GetEmailEstimates]()
RETURNS TABLE 
AS
RETURN 
(
	select			IAEmailTemplate.IAEmailTemplateID,
					IAEmailTemplate.Name,
					IAEmailTemplate.Body,
					IAEmailTemplate.CreatedDateTime
	from			IAEmailTemplate
)
GO
