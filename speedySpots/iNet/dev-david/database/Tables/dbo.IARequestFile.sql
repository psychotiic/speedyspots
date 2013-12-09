CREATE TABLE [dbo].[IARequestFile]
(
[IARequestFileID] [int] NOT NULL IDENTITY(1, 1),
[IARequestID] [int] NOT NULL CONSTRAINT [DF_IARequestFile_IARequestID] DEFAULT ((0)),
[Filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestFile_Filename] DEFAULT (''),
[FilenameUnique] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Table_1_OriginalFilename] DEFAULT (''),
[FileSize] [int] NOT NULL CONSTRAINT [DF_IARequestFile_FileSize] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestFile_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestFile] ADD CONSTRAINT [PK_IARequestFile] PRIMARY KEY CLUSTERED  ([IARequestFileID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestFile] ADD CONSTRAINT [FK_IARequestFile_IARequest] FOREIGN KEY ([IARequestID]) REFERENCES [dbo].[IARequest] ([IARequestID]) ON DELETE CASCADE
GO
