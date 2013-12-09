CREATE TABLE [dbo].[IAMusic]
(
[IAMusicID] [int] NOT NULL IDENTITY(1, 1),
[IAMusicGroupID] [int] NOT NULL,
[IAMusicTempoID] [int] NOT NULL,
[Path] [nvarchar] (56) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Filename] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Extension] [nvarchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LengthSecs] [smallint] NOT NULL,
[LengthBeds] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreateDateTime] [datetime] NOT NULL,
[FilenameDisplay] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAMusic_DisplayFilename] DEFAULT ('')
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMusic] ADD CONSTRAINT [PK_IAMusic] PRIMARY KEY CLUSTERED  ([IAMusicID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMusic] ADD CONSTRAINT [FK_IAMusic_IAMusicGroup] FOREIGN KEY ([IAMusicGroupID]) REFERENCES [dbo].[IAMusicGroup] ([IAMusicGroupID])
GO
ALTER TABLE [dbo].[IAMusic] ADD CONSTRAINT [FK_IAMusic_IAMusicTempo] FOREIGN KEY ([IAMusicTempoID]) REFERENCES [dbo].[IAMusicTempo] ([IAMusicTempoID])
GO
