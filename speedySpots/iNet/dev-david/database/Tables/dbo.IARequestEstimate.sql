CREATE TABLE [dbo].[IARequestEstimate]
(
[IARequestEstimateID] [int] NOT NULL IDENTITY(1, 1),
[IARequestID] [int] NOT NULL CONSTRAINT [DF_IARequestEstimate_IARequestID] DEFAULT ((0)),
[IAOrderID] [int] NOT NULL CONSTRAINT [DF_IARequestEstimate_IAOrderID] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IARequestEstimate_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[EmailRecipient] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Table_1_RecipientAddress] DEFAULT (''),
[EmailCC] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Table_1_RecipientCC] DEFAULT (''),
[EmailSubject] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestEstimate_EmailSubject] DEFAULT (''),
[EmailBody] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestEstimate_EmailBody] DEFAULT (''),
[Charge] [money] NOT NULL CONSTRAINT [DF_IARequestEstimate_Charge] DEFAULT (''),
[IsPaymentRequired] [bit] NOT NULL CONSTRAINT [DF_IARequestEstimate_IsPaymentRequired] DEFAULT ((0)),
[IsApproved] [bit] NOT NULL CONSTRAINT [DF_IARequestEstimate_IsApproved] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestEstimate_CreatedDateTime] DEFAULT (''),
[ApprovedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestEstimate_ApprovedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestEstimate] ADD CONSTRAINT [PK_IARequestEstimate] PRIMARY KEY CLUSTERED  ([IARequestEstimateID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestEstimate] WITH NOCHECK ADD CONSTRAINT [FK_IARequestEstimate_IAOrder] FOREIGN KEY ([IAOrderID]) REFERENCES [dbo].[IAOrder] ([IAOrderID])
GO
ALTER TABLE [dbo].[IARequestEstimate] ADD CONSTRAINT [FK_IARequestEstimate_IARequest] FOREIGN KEY ([IARequestID]) REFERENCES [dbo].[IARequest] ([IARequestID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IARequestEstimate] ADD CONSTRAINT [FK_IARequestEstimate_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID])
GO
ALTER TABLE [dbo].[IARequestEstimate] NOCHECK CONSTRAINT [FK_IARequestEstimate_IAOrder]
GO
