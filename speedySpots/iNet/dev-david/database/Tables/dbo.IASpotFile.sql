CREATE TABLE [dbo].[IASpotFile]
(
[IASpotFileID] [int] NOT NULL IDENTITY(1, 1),
[IASpotID] [int] NOT NULL CONSTRAINT [DF_IASpotFile_IASpotID] DEFAULT ((0)),
[IASpotFileTypeID] [int] NOT NULL CONSTRAINT [DF_IASpotFile_IAFileTypeID] DEFAULT ((0)),
[Filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpotFile_Filename] DEFAULT (''),
[FilenameUnique] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IASpotFile_FilenameUnique] DEFAULT (''),
[FileSize] [int] NOT NULL CONSTRAINT [DF_IASpotFile_FileSize] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IASpotFile_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IASpotFile] ADD CONSTRAINT [PK_IASpotFile] PRIMARY KEY CLUSTERED  ([IASpotFileID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IASpotFile] ADD CONSTRAINT [FK_IASpotFile_IASpotFileType] FOREIGN KEY ([IASpotFileTypeID]) REFERENCES [dbo].[IASpotFileType] ([IASpotFileTypeID])
GO
ALTER TABLE [dbo].[IASpotFile] ADD CONSTRAINT [FK_IASpotFile_IASpot] FOREIGN KEY ([IASpotID]) REFERENCES [dbo].[IASpot] ([IASpotID]) ON DELETE CASCADE
GO
