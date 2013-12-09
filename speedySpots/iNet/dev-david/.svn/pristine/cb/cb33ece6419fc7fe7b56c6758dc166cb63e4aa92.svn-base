CREATE TABLE [dbo].[IAMusicUserRating]
(
[IAMusicUserRatingID] [int] NOT NULL IDENTITY(1, 1),
[IAMusicID] [int] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL,
[Rating] [smallint] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMusicUserRating] ADD CONSTRAINT [PK_IAMusicUserRating] PRIMARY KEY CLUSTERED  ([IAMusicUserRatingID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAMusicUserRating] ADD CONSTRAINT [FK_IAMusicUserRating_IAMusic] FOREIGN KEY ([IAMusicID]) REFERENCES [dbo].[IAMusic] ([IAMusicID])
GO
ALTER TABLE [dbo].[IAMusicUserRating] ADD CONSTRAINT [FK_IAMusicUserRating_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID])
GO
