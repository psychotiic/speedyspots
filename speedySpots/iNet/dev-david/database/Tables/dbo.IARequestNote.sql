CREATE TABLE [dbo].[IARequestNote]
(
[IARequestNoteID] [int] NOT NULL IDENTITY(1, 1),
[IARequestID] [int] NOT NULL CONSTRAINT [DF_IARequestNote_IARequestID] DEFAULT ((0)),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IARequestNote_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Note] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestNote_Note] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestNote_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestNote] ADD CONSTRAINT [PK_IARequestNote] PRIMARY KEY CLUSTERED  ([IARequestNoteID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestNote] ADD CONSTRAINT [FK_IARequestNote_IARequest] FOREIGN KEY ([IARequestID]) REFERENCES [dbo].[IARequest] ([IARequestID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IARequestNote] ADD CONSTRAINT [FK_IARequestNote_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID])
GO
