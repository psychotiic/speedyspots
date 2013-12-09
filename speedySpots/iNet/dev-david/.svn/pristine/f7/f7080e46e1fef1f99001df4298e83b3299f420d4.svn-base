CREATE TABLE [dbo].[IAEmailTemplate]
(
[IAEmailTemplateID] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAEmailTemplate_Name] DEFAULT (''),
[Body] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAEmailTemplate_Body] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAEmailTemplate_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAEmailTemplate] ADD CONSTRAINT [PK_IAEmailTemplate] PRIMARY KEY CLUSTERED  ([IAEmailTemplateID]) ON [PRIMARY]
GO
