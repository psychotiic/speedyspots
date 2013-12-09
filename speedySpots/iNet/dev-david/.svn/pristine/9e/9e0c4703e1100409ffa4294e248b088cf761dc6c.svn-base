CREATE TABLE [dbo].[IAUserSettingsGrid]
(
[IAUserSettingsGridID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAUserSettingsGrid_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAUserSettingsGrid_GridName_1] DEFAULT (''),
[SortExpression] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAUserSettingsGrid_SortExpression] DEFAULT (''),
[Filters] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAUserSettingsGrid_Filters] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAUserSettingsGrid] ADD CONSTRAINT [PK_IAUserSettingsGrid] PRIMARY KEY CLUSTERED  ([IAUserSettingsGridID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAUserSettingsGrid] ADD CONSTRAINT [FK_IAUserSettingsGrid_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
