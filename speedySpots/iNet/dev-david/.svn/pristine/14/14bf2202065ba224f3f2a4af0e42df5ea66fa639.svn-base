CREATE TABLE [dbo].[MPRole]
(
[MPRoleID] [uniqueidentifier] NOT NULL,
[MPOrgID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MPRole_MPOrgGUID] DEFAULT ('00000000-0000-0000-0000-000000000000'),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Role_Name] DEFAULT (''),
[Description] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF_Role_Description] DEFAULT (''),
[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_MPRole_CreatedOn] DEFAULT (getdate())
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRole] ADD CONSTRAINT [PK_MPRole] PRIMARY KEY CLUSTERED  ([MPRoleID]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MPRole] ADD CONSTRAINT [FK_MPRole_MPOrg] FOREIGN KEY ([MPOrgID]) REFERENCES [dbo].[MPOrg] ([MPOrgID]) ON DELETE CASCADE
GO
EXEC sp_addextendedproperty N'MS_Description', N'MemberProtect - Holds security role information, roles consist of privileges and users are assigned various roles', 'SCHEMA', N'dbo', 'TABLE', N'MPRole', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The role description', 'SCHEMA', N'dbo', 'TABLE', N'MPRole', 'COLUMN', N'Description'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The role name', 'SCHEMA', N'dbo', 'TABLE', N'MPRole', 'COLUMN', N'Name'
GO
