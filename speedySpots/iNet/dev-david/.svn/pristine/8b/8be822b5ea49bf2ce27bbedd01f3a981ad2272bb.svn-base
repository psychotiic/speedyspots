CREATE TABLE [dbo].[MPUserRBAQuestion]
(
[MPUserRBAQuestionD] [uniqueidentifier] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPUserRBAQuestion_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPRBAQuestionID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPUserRBAQuestion_MPRBAQuestionGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Answer] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_UserRBAQuestion_Answer] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPUserRBAQuestion_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserRBAQuestion] ADD CONSTRAINT [PK_MPUserRBAQuestion] PRIMARY KEY CLUSTERED  ([MPUserRBAQuestionD]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserRBAQuestion] ADD CONSTRAINT [FK_MPUserRBAQuestion_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds RBA question information for users, including their answers to their chosen questions', 'SCHEMA', N'dbo', 'TABLE', N'MPUserRBAQuestion', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The answer to the users RBA question', 'SCHEMA', N'dbo', 'TABLE', N'MPUserRBAQuestion', 'COLUMN', N'Answer'
GO
