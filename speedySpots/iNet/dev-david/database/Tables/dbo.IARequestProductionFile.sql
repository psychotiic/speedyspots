CREATE TABLE [dbo].[IARequestProductionFile]
(
[IARequestProductionFileID] [int] NOT NULL IDENTITY(1, 1),
[IARequestProductionID] [int] NOT NULL CONSTRAINT [DF_IARequestProductionFile_IARequestProductionID] DEFAULT ((0)),
[Filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestProductionFile_Filename] DEFAULT (''),
[FilenameUnique] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IARequestProductionFile_FilenameUnique] DEFAULT (''),
[FileSize] [int] NOT NULL CONSTRAINT [DF_IARequestProductionFile_FileSize] DEFAULT ((0)),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IARequestProductionFile_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestProductionFile] ADD CONSTRAINT [PK_IARequestProductionFile] PRIMARY KEY CLUSTERED  ([IARequestProductionFileID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IARequestProductionFile] ADD CONSTRAINT [FK_IARequestProductionFile_IARequestProduction] FOREIGN KEY ([IARequestProductionID]) REFERENCES [dbo].[IARequestProduction] ([IARequestProductionID]) ON DELETE CASCADE
GO
