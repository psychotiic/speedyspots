CREATE TABLE [dbo].[IAOrder]
(
[IAOrderID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAOrder_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[CreditCardType] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardType] DEFAULT (''),
[CreditCardNumber] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardNumber] DEFAULT (''),
[CreditCardExpirationMonth] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardExpirationMonth] DEFAULT (''),
[CreditCardExpirationYear] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardExpirationYear] DEFAULT (''),
[CreditCardFirstName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardFirstName] DEFAULT (''),
[CreditCardLastName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardLastName] DEFAULT (''),
[CreditCardZip] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_CreditCardBillingZip] DEFAULT (''),
[AuthorizeNetTransactionID] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetTransactionID] DEFAULT (''),
[AuthorizeNetResponse] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetResponse] DEFAULT (''),
[AuthorizeNetProcessUsername] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetProcessUsername] DEFAULT (''),
[AuthorizeNetProcessDatetime] [datetime] NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetProcessDatetime] DEFAULT (''),
[AuthorizeNetProcessResponse] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetProcessResponse] DEFAULT (''),
[AuthorizeNetProcessCaptureAmount] [decimal] (18, 0) NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetProcessCaptureAmount] DEFAULT ((0)),
[AuthorizeNetProcessCaptureAmountChangeReason] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetProcessCaptureAmountChangeReason] DEFAULT (''),
[AuthorizeNetProcessStatus] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAOrder_AuthorizeNetProcessStatus] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAOrder_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAOrder] ADD CONSTRAINT [PK_IAOrder] PRIMARY KEY CLUSTERED  ([IAOrderID]) ON [PRIMARY]
GO
