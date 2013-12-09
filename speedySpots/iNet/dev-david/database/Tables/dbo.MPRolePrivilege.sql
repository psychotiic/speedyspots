CREATE TABLE [dbo].[MPRolePrivilege]
(
[MPRolePrivilegeID] [uniqueidentifier] NOT NULL,
[MPRoleID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPRolePrivilege_MPRoleGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[PrivilegeCode] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_RolePrivilege_PrivilegeCode] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPRolePrivilege_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRolePrivilege] ADD CONSTRAINT [PK_MPRolePrivilege] PRIMARY KEY CLUSTERED  ([MPRolePrivilegeID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRolePrivilege] ADD CONSTRAINT [FK_MPRolePrivilege_MPRole] FOREIGN KEY ([MPRoleID]) REFERENCES [dbo].[MPRole] ([MPRoleID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - A joiner table between Role and Privilege', 'SCHEMA', N'dbo', 'TABLE', N'MPRolePrivilege', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The code associated with Privilege.Code, these MUST match, they don''t use ID''s cause they must be static', 'SCHEMA', N'dbo', 'TABLE', N'MPRolePrivilege', 'COLUMN', N'PrivilegeCode'
GO
