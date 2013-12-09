CREATE TABLE [dbo].[IAPaymentLineItem]
(
[IAPaymentLineItemID] [int] NOT NULL IDENTITY(1, 1),
[IAPaymentID] [int] NOT NULL CONSTRAINT [DF_IAPaymentLineItem_IAPaymentID] DEFAULT ((0)),
[QBInvoiceID] [int] NOT NULL CONSTRAINT [DF_IAPaymentLineItem_QBInvoiceID] DEFAULT ((0)),
[Invoice] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPaymentLineItem_Invoice] DEFAULT (''),
[Amount] [decimal] (18, 2) NOT NULL CONSTRAINT [DF_IAPaymentLineItem_Amount] DEFAULT ((0))
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAPaymentLineItem] ADD CONSTRAINT [PK_IAPaymentLineItem] PRIMARY KEY CLUSTERED  ([IAPaymentLineItemID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAPaymentLineItem] ADD CONSTRAINT [FK_IAPaymentLineItem_IAPayment] FOREIGN KEY ([IAPaymentID]) REFERENCES [dbo].[IAPayment] ([IAPaymentID]) ON DELETE CASCADE
GO
