CREATE TABLE [dbo].[IACustomerCreditCard]
(
[IACustomerCreditCardID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IACustomerCreditCard_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_Name] DEFAULT (''),
[CreditCardType] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_CreditCardType] DEFAULT (''),
[CreditCardNumber] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_CreditCardNumber] DEFAULT (''),
[CreditCardExpirationMonth] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_CreditCardExpirationMonth] DEFAULT (''),
[CreditCardExpirationYear] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_CreditCardExpirationYear] DEFAULT (''),
[CreditCardFirstName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_CreditCardFirstName] DEFAULT (''),
[CreditCardLastName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_CreditCardLastName] DEFAULT (''),
[CreditCardZip] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IACustomerCreditCard_BillingZip] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IACustomerCreditCard] ADD CONSTRAINT [PK_IACustomerCreditCard] PRIMARY KEY CLUSTERED  ([IACustomerCreditCardID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IACustomerCreditCard] ADD CONSTRAINT [FK_IACustomerCreditCard_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
