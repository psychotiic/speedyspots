CREATE TABLE [dbo].[IAFeedback]
(
[IAFeedbackID] [int] NOT NULL IDENTITY(1, 1),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_IAFeedback_MPUserID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Type] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAFeedback_Type] DEFAULT (''),
[Feeling] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAFeedback_Feeling] DEFAULT (''),
[Message] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAFeedback_Message] DEFAULT (''),
[Url] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAFeedback_Url] DEFAULT (''),
[Filename] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_IAFeedback_Page] DEFAULT (''),
[CreatedDateTime] [datetime] NOT NULL CONSTRAINT [DF_IAFeedback_CreatedDateTime] DEFAULT ('')
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[IAFeedback] ADD CONSTRAINT [PK_IAFeedback] PRIMARY KEY CLUSTERED  ([IAFeedbackID]) ON [PRIMARY]
GO
