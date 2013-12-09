CREATE TABLE [dbo].[IAPayment]
(
[IAPaymentID] [int] NOT NULL IDENTITY(1, 1),
[Total] [decimal] (18, 2) NOT NULL CONSTRAINT [DF_IAPayment_Total] DEFAULT ((0)),
[Email] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_Email] DEFAULT (''),
[Company] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_Company] DEFAULT (''),
[Instructions] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_Instructions] DEFAULT (''),
[CreditCardType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_CreditCardType] DEFAULT (''),
[CreditCardNumber] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_CreditCardNumber] DEFAULT (''),
[Zip] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_Zip] DEFAULT (''),
[AuthorizeNetTransactionID] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_AuthorizeNetTransactionID] DEFAULT (''),
[AuthorizeNetProcessResponse] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAPayment_AuthorizeNetProcessResponse] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAPayment_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAPayment] ADD CONSTRAINT [PK_IAPayment] PRIMARY KEY CLUSTERED  ([IAPaymentID]) ON [PRIMARY]
GO
