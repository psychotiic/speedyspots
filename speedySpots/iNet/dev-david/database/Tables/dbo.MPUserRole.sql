CREATE TABLE [dbo].[MPUserRole]
(
[MPUserRoleID] [uniqueidentifier] NOT NULL,
[MPUserID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPUserRole_MPUserGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[MPRoleID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPUserRole_MPRoleGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPUserRole_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserRole] ADD CONSTRAINT [PK_MPUserRole] PRIMARY KEY CLUSTERED  ([MPUserRoleID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPUserRole] ADD CONSTRAINT [FK_MPUserRole_MPRole] FOREIGN KEY ([MPRoleID]) REFERENCES [dbo].[MPRole] ([MPRoleID]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MPUserRole] ADD CONSTRAINT [FK_MPUserRole_MPUser] FOREIGN KEY ([MPUserID]) REFERENCES [dbo].[MPUser] ([MPUserID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - A joiner table between User and Role', 'SCHEMA', N'dbo', 'TABLE', N'MPUserRole', NULL, NULL
GO
