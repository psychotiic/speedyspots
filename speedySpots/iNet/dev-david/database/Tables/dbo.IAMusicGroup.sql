CREATE TABLE [dbo].[IAMusicGroup]
(
[IAMusicGroupID] [int] NOT NULL,
[Name] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMusicGroup] ADD CONSTRAINT [PK_IAMusicGroup] PRIMARY KEY CLUSTERED  ([IAMusicGroupID]) ON [PRIMARY]
GO
