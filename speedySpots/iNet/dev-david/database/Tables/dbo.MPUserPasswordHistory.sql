CREATE TABLE [dbo].[MPUserPasswordHistory]
(
[MPUserPasswordHistoryID] [uniqueidentifier] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPUserPasswordHistory_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Password] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_UserPasswordHistory_Password] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_UserPasswordHistory_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserPasswordHistory] ADD CONSTRAINT [PK_MPUserPasswordHistory] PRIMARY KEY CLUSTERED  ([MPUserPasswordHistoryID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserPasswordHistory] ADD CONSTRAINT [FK_MPUserPasswordHistory_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds historial password information for users, eg. the last X number of passwords so they can''t use the same password more than once over a time period', 'SCHEMA', N'dbo', 'TABLE', N'MPUserPasswordHistory', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The encrypted password value already used', 'SCHEMA', N'dbo', 'TABLE', N'MPUserPasswordHistory', 'COLUMN', N'Password'
GO
