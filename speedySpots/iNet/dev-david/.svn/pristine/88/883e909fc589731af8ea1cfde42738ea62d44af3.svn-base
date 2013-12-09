CREATE TABLE [dbo].[IARequest]
(
[IARequestID] [int] NOT NULL IDENTITY(1, 1),
[IARequestStatusID] [int] NOT NULL CONSTRAINT [DF_IARequest_IARequestStatusID] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IARequest_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPUserIDOwnedByStaff] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IARequest_MPUserIDOwnedBy] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[ContactPhone] [nvarchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequest_ContactPhone] DEFAULT (''),
[ContactPhoneExtension] [nvarchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequest_ContactPhoneExtension] DEFAULT (''),
[Script] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequest_Script] DEFAULT (''),
[ProductionNotes] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequest_ProductionNotes] DEFAULT (''),
[NotificationEmails] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequest_NotificationEmails] DEFAULT (''),
[IsRushOrder] [bit] NOT NULL CONSTRAINT [DF_IARequest_IsRushOrder] DEFAULT ((0)),
[HasBeenViewedByProducer] [bit] NOT NULL CONSTRAINT [DF_IARequest_HasBeenViewedByProducer] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequest_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequest] ADD CONSTRAINT [PK_IARequest] PRIMARY KEY CLUSTERED  ([IARequestID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequest] ADD CONSTRAINT [FK_IARequest_IARequestStatus] FOREIGN KEY ([IARequestStatusID]) REFERENCES [dbo].[IARequestStatus] ([IARequestStatusID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IARequest] ADD CONSTRAINT [FK_IARequest_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID])
GO
