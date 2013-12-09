CREATE TABLE [dbo].[MPOrgUser]
(
[MPOrgUserID] [uniqueidentifier] NOT NULL,
[MPOrgID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPOrgUser_MPOrgGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPOrgUser_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPOrgUser_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrgUser] ADD CONSTRAINT [PK_MPOrgUser] PRIMARY KEY CLUSTERED  ([MPOrgUserID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPOrgUser] ADD CONSTRAINT [FK_MPOrgUser_MPOrg] FOREIGN KEY ([MPOrgID]) REFERENCES [dbo].[MPOrg] ([MPOrgID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds user/organization associations within MemberProtect, a joiner table', 'SCHEMA', N'dbo', 'TABLE', N'MPOrgUser', NULL, NULL
GO
