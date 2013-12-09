CREATE TABLE [dbo].[IAMessage]
(
[IAMessageID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAMessage_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[DisplayStartDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_FromDateTime] DEFAULT (''),
[DisplayEndDateTime] [datetime] NOT NULL CONSTRAINT [DF_Table_1_ToDateTime] DEFAULT (''),
[Subject] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAMessage_Subject] DEFAULT (''),
[Body] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAMessage_Body] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAMessage_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMessage] ADD CONSTRAINT [PK_IAMessage] PRIMARY KEY CLUSTERED  ([IAMessageID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMessage] ADD CONSTRAINT [FK_IAMessage_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID])
GO
