CREATE TABLE [dbo].[QBInvoice]
(
[QBInvoiceID] [int] NOT NULL IDENTITY(1, 1),
[IAJobID] [int] NOT NULL CONSTRAINT [DF_QBInvoice_IAJobID] DEFAULT ((0)),
[QBInvoiceRefID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_QBInvoice_QuickbooksInvoiceID] DEFAULT (''),
[QBCustomerRefID] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_QBInvoice_QuickbooksCustomerID] DEFAULT (''),
[InvoiceNumber] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_QBInvoice_InvoiceNumber] DEFAULT (''),
[Amount] [decimal] (18, 2) NOT NULL CONSTRAINT [DF_QBInvoice_Amount] DEFAULT ((0)),
[Filename] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_QBInvoice_Filename] DEFAULT (''),
[DueDateTime] [datetime] NOT NULL CONSTRAINT [DF_QBInvoice_DueDateTime] DEFAULT (''),
[ImportedDateTime] [datetime] NOT NULL CONSTRAINT [DF_QBInvoice_ImportedDateTime] DEFAULT ('')
) ON [PRIMARY]
ALTER TABLE [dbo].[QBInvoice] ADD
CONSTRAINT [FK_QBInvoice_IAJob] FOREIGN KEY ([IAJobID]) REFERENCES [dbo].[IAJob] ([IAJobID])
GO
ALTER TABLE [dbo].[QBInvoice] ADD CONSTRAINT [PK_QBInvoice] PRIMARY KEY CLUSTERED  ([QBInvoiceID]) ON [PRIMARY]
GO
