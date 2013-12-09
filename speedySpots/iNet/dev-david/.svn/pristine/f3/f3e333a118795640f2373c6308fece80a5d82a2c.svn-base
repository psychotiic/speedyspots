CREATE TABLE [dbo].[MPVersion]
(
[MPVersionID] [uniqueidentifier] NOT NULL,
[Version] [datetime] NOT NULL CONSTRAINT [DF_Version_Version] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_Version_CreatedDateTime] DEFAULT (getdate()),
[Script] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Version_Script] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPVersion] ADD CONSTRAINT [PK_MPVersion] PRIMARY KEY CLUSTERED  ([MPVersionID]) ON [PRIMARY]
GO
