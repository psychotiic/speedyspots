CREATE TABLE [dbo].[IAMusicTempo]
(
[IAMusicTempoID] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Display] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMusicTempo] ADD CONSTRAINT [PK_IAMusicTempo] PRIMARY KEY CLUSTERED  ([IAMusicTempoID]) ON [PRIMARY]
GO
