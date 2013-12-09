SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE FUNCTION [dbo].[fn_Customer_GetInvoices](@IARequestID int)
RETURNS TABLE 
AS
RETURN 
(
	select		QBInvoice.QBInvoiceID,
				QBInvoice.InvoiceNumber,
				IAJob.Name,
				IAJob.Sequence,
				QBInvoice.DueDateTime,
				QBInvoice.Amount,
				QBInvoice.Filename
	from		IAJob
	join		QBInvoice on
				QBInvoice.IAJobID = IAJob.IAJobID
	join		IARequest on
				IARequest.IARequestID = IAJob.IARequestID
	where		IAJob.IARequestID = @IARequestID				
)
GO
