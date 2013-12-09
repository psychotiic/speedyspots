CREATE TABLE [dbo].[MPArchiveTable]
(
[MPArchiveTableID] [uniqueidentifier] NOT NULL,
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_ArchiveTable_CreatedOn] DEFAULT (getdate()),
[TableName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_ArchiveTable_TableName] DEFAULT (''),
[ArchiveTableName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_ArchiveTable_ArchiveTableName] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPArchiveTable] ADD CONSTRAINT [PK_MPArchiveTable] PRIMARY KEY CLUSTERED  ([MPArchiveTableID]) ON [PRIMARY]
GO
