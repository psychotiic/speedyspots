CREATE TABLE [dbo].[IAMessageRecipient]
(
[IAMessageRecipientID] [int] NOT NULL IDENTITY(1, 1),
[IAMessageID] [int] NOT NULL CONSTRAINT [DF_IAMessageRecipient_IAMessageID] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAMessageRecipient_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[IsAcknowledged] [bit] NOT NULL CONSTRAINT [DF_IAMessageRecipient_IsAcknowledged] DEFAULT ((0)),
[AcknowledgedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAMessageRecipient_AcknowledgedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMessageRecipient] ADD CONSTRAINT [PK_IAMessageRecipient] PRIMARY KEY CLUSTERED  ([IAMessageRecipientID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMessageRecipient] ADD CONSTRAINT [FK_IAMessageRecipient_IAMessage1] FOREIGN KEY ([IAMessageID]) REFERENCES [dbo].[IAMessage] ([IAMessageID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IAMessageRecipient] ADD CONSTRAINT [FK_IAMessageRecipient_IAMessage] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
