CREATE TABLE [dbo].[IAJobFile]
(
[IAJobFileID] [int] NOT NULL IDENTITY(1, 1),
[IAJobID] [int] NOT NULL CONSTRAINT [DF_IAJobFile_IAJobID] DEFAULT ((0)),
[IAJobFileTypeID] [int] NOT NULL CONSTRAINT [DF_IAJobFile_IAJobFileTypeID] DEFAULT ((0)),
[Filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAJobFile_Filename] DEFAULT (''),
[FilenameUnique] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAJobFile_FilenameUnique] DEFAULT (''),
[FileSize] [int] NOT NULL CONSTRAINT [DF_IAJobFile_FileSize] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAJobFile_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAJobFile] ADD CONSTRAINT [PK_IAJobFile] PRIMARY KEY CLUSTERED  ([IAJobFileID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAJobFile] ADD CONSTRAINT [FK_IAJobFile_IAJobFileType] FOREIGN KEY ([IAJobFileTypeID]) REFERENCES [dbo].[IAJobFileType] ([IAJobFileTypeID])
GO
ALTER TABLE [dbo].[IAJobFile] ADD CONSTRAINT [FK_IAJobFile_IAJob] FOREIGN KEY ([IAJobID]) REFERENCES [dbo].[IAJob] ([IAJobID]) ON DELETE CASCADE
GO
