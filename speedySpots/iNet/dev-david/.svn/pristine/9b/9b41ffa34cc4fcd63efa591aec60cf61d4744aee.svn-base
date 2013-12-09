CREATE TABLE [dbo].[MPRBAQuestion]
(
[MPRBAQuestionID] [uniqueidentifier] NOT NULL,
[Question] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_RBAQuestion_Question] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPRBAQuestion_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRBAQuestion] ADD CONSTRAINT [PK_MPRBAQuestion] PRIMARY KEY CLUSTERED  ([MPRBAQuestionID]) ON [PRIMARY]
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds RBA question information, these questions can be chosen by users as part of their security profile', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAQuestion', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The RBA security questions used within system RBA security', 'SCHEMA', N'dbo', 'TABLE', N'MPRBAQuestion', 'COLUMN', N'Question'
GO
